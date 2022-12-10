using System;
using PixelCrew.Creatures.Core;
using PixelCrew.Creatures.Core.Health;
using PixelCrew.Creatures.Hero.Features;
using PixelCrew.Creatures.Hero.Weapons;
using PixelCrew.Model;
using PixelCrew.Model.Data;
using PixelCrew.Model.Definitions;
using PixelCrew.Model.Definitions.Player;
using PixelCrew.Model.Definitions.Repository;
using PixelCrew.Model.Definitions.Repository.Items;
using PixelCrew.UI.Windows.Inventory;
using UnityEngine;
using UnityEngine.Events;

namespace PixelCrew.Creatures.Hero
{
    [RequireComponent(typeof(HeroCoinScore))]
    [RequireComponent(typeof(HeroAttacker))]
    [RequireComponent(typeof(HeroJumper))]
    [RequireComponent(typeof(MovementComponent))]
    [RequireComponent(typeof(HeroInteract))]
    [RequireComponent(typeof(HealthComponent))]
    [RequireComponent(typeof(ProjectileThrower))]
    [RequireComponent(typeof(DashComponent))]
    [RequireComponent(typeof(GetOffThePlatformComponent))]
    public class Hero : Creature
    {
        [SerializeField] private HeroInputReader _reader;
        [SerializeField] private HeroFlashLight _flashLight;

        private GetOffThePlatformComponent _getOffThePlatform;
        private GameSession _session;
        private HeroCoinScore _coinScore;
        private HeroJumper _jumper;
        private HeroInteract _interact;
        private HealthComponent _health;
        private ProjectileThrower _thrower;
        private HeroAttacker _attacker;
        private ShieldComponent _shield;
        private DashComponent _dash;
        private FullInventoryController _fullInventoryController;

        private InventoryItemData SelectedItem => _session.QuickInventory.SelectedItem;

        protected override void Awake()
        {
            base.Awake();
            _coinScore = GetComponent<HeroCoinScore>();
            _jumper = GetComponent<HeroJumper>();
            _interact = GetComponent<HeroInteract>();
            _health = GetComponent<HealthComponent>();
            _thrower = GetComponent<ProjectileThrower>();
            _shield = GetComponentInChildren<ShieldComponent>();
            _dash = GetComponent<DashComponent>();
            _getOffThePlatform = GetComponent<GetOffThePlatformComponent>();
        }

        private void Start()
        {
            _session = FindObjectOfType<GameSession>();
            _fullInventoryController = FindObjectOfType<FullInventoryController>();
            _session.Data.Inventory.OnChanged += OnInventoryChanged;
            _session.StatsModel.OnUpgraded += OnHeroUpgraded;

            _coinScore.Init(_session);
            _jumper.Init(Animator, Rigidbody, _session);
            _thrower.Init(Animator, Sounds, _session);
            // _health.SetOriginHealth(_session.Data.Health.Value);

            _attacker = Attacker as HeroAttacker;

            if (_attacker != null)
            {
                _attacker.Init(Animator, _session);
                _reader.Init(Movement, _jumper, _interact, _attacker, _thrower, this);
            }
        }

        private void OnHeroUpgraded(StatId id)
        {
            switch (id)
            {
                case StatId.Hp:
                    var health = (int)_session.StatsModel.GetValue(id);
                    _session.Data.Health.Value = health;
                    _health.SetOriginHealth(health);
                    break;
                case StatId.Speed:
                    Movement.SetSpeed(_session.StatsModel.GetValue(StatId.Speed));
                    break;
                case StatId.RangeDamage:
                    break;
                case StatId.CriticalDamage:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(id), id, null);
            }
        }

        private void OnDestroy()
        {
            _session.Data.Inventory.OnChanged -= OnInventoryChanged;
        }

        private void OnDrawGizmos()
        {
            Debug.DrawLine(transform.position, transform.position + Vector3.down * 0.5f, Color.green);
        }

        public void OnHealthChanged(int value)
        {
            _session.Data.Health.Value = value;
        }

        public void AddInInventory(string id, int value, UnityEvent callback = null)
        {
            _session.Data.Inventory.Add(id, value, callback);
        }

        public void ToggleFlashLight()
        {
            var isActive = _flashLight.gameObject.activeSelf;
            _flashLight.gameObject.SetActive(!isActive);
        }

        public void NextItem()
        {
            if (_fullInventoryController.gameObject.activeSelf == true)
                _session.FullInventoryModel.SetNextItem();
            else
                _session.QuickInventory.SetNextItem();
        }

        public void OnUseItem()
        {
            if (IsSelectedItem(ItemTag.Throwable))
                _thrower.TryThrow();
            else if (IsSelectedItem(ItemTag.Potion))
                UsePotion();
        }

        public void OnGetOffPlatform()
        {
            StartCoroutine(_getOffThePlatform.TryGetOff());
        }

        public void OnUsePerk()
        {
            if (_session.PerksModel.IsSuperShieldSupported)
            {
                _shield.TryUse();
            }
            else if (_session.PerksModel.IsDashSupported)
            {
                _dash.Use();
            }
        }

        private void OnInventoryChanged(string id, int value)
        {
            if (id == "Sword")
                Invoke(nameof(UpdateHeroWeaponInTime), 0.14f);
        }

        private bool IsSelectedItem(ItemTag itemTag)
        {
            return _session.QuickInventory.SelectedItemDefinition.HasTag(itemTag);
        }

        private void UpdateHeroWeaponInTime()
        {
            _attacker.UpdateHeroWeapon();
        }

        private void UsePotion()
        {
            var potion = DefinitionsFacade.Instance.PotionRepository.Get(SelectedItem.Id);

            switch (potion.Effect)
            {
                case PotionEffect.AddHp:
                    _session.Data.Health.Value += potion.Value;
                    break;
                case PotionEffect.SpeedUp:
                    if (Movement.IsSpeedUpWork == true)
                        return;

                    StartCoroutine(Movement.SpeedUp(potion.Value, potion.Time));
                    break;
            }

            _session.Data.Inventory.Remove(SelectedItem.Id, 1);
        }
    }
}