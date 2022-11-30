using PixelCrew.Creatures.Core;
using PixelCrew.Creatures.Core.Health;
using PixelCrew.Model;
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

        protected override void Awake()
        {
            base.Awake();
            _coinScore = GetComponent<HeroCoinScore>();
            _jumper = GetComponent<HeroJumper>();
            _interact = GetComponent<HeroInteract>();
            _health = GetComponent<HealthComponent>();
            _thrower = GetComponent<ProjectileThrower>();
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

        public void TryHeal()
        {
            const string SpellItem = "Spell";
            var inventory = _session.Data.Inventory;

            if (inventory.Count(SpellItem) > 0)
            {
                inventory.Remove(SpellItem, 1);
                _health.ModifyHealth(gameObject, 1);
            }
        }

        public void AddInInventory(string id, int value, UnityEvent callback = null)
        {
            _session.Data.Inventory.Add(id, value, callback);
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