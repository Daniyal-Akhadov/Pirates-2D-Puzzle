using PixelCrew.Components;
using PixelCrew.Creatures.Core;
using PixelCrew.Creatures.Core.Health;
using PixelCrew.Model;
using PixelCrew.Model.Data;
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
    public class Hero : Creature
    {
        [SerializeField] private HeroInputReader _reader;

        private GameSession _session;
        private HeroCoinScore _coinScore;
        private HeroJumper _jumper;
        private HeroInteract _interact;
        private HealthComponent _health;
        private ProjectileThrower _thrower;
        private HeroAttacker _attacker;
        private SpeedUpComponent _speedUp;

        private InventoryItemData SelectedItem => _session.QuickInventory.SelectedItem;

        protected override void Awake()
        {
            base.Awake();
            _coinScore = GetComponent<HeroCoinScore>();
            _jumper = GetComponent<HeroJumper>();
            _interact = GetComponent<HeroInteract>();
            _health = GetComponent<HealthComponent>();
            _thrower = GetComponent<ProjectileThrower>();
            _speedUp = GetComponent<SpeedUpComponent>();
        }

        private void Start()
        {
            _session = FindObjectOfType<GameSession>();
            _session.Data.Inventory.OnChanged += OnInventoryChanged;

            _coinScore.Init(_session);
            _jumper.Init(Animator, Rigidbody);
            _thrower.Init(Animator, Sounds, _session);
            _health.SetOriginHealth(_session.Data.Health.Value);

            _attacker = Attacker as HeroAttacker;

            if (_attacker != null)
            {
                _attacker.Init(Animator, _session);
                _reader.Init(Movement, _jumper, _interact, _attacker, _thrower, this);
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

        private bool TrySpeedUp()
        {
            if (_speedUp.IsWork == true || SelectedItem.Id != "SpeedSpell")
                return false;

            _speedUp.Do(gameObject);
            _session.Data.Inventory.Remove(SelectedItem.Id, 1);
            return true;
        }

        private bool TryHeal()
        {
            const string Spell = "Spell";
            const string BigSpell = "BigSpell";

            if (SelectedItem.Id is Spell or BigSpell == false)
                return false;

            switch (SelectedItem.Id)
            {
                case Spell:
                    _health.ModifyHealth(gameObject, 1);
                    break;
                case BigSpell:
                    _health.ModifyHealth(gameObject, 3);
                    break;
            }

            _session.Data.Inventory.Remove(SelectedItem.Id, 1);
            return true;
        }

        public void AddInInventory(string id, int value, UnityEvent callback = null)
        {
            _session.Data.Inventory.Add(id, value, callback);
        }

        public void NextItem()
        {
            _session.QuickInventory.SetNextItem();
        }

        public void OnUseItem()
        {
            if (SelectedItem.Id == "SpeedSpell")
            {
                TrySpeedUp();
            }
            else
            {
                TryHeal();
            }
        }

        private void OnInventoryChanged(string id, int value)
        {
            if (id == "Sword")
                Invoke(nameof(UpdateHeroWeaponInTime), 0.14f);
        }

        private void UpdateHeroWeaponInTime()
        {
            _attacker.UpdateHeroWeapon();
        }
    }
}