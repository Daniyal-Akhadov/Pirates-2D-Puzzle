using PixelCrew.Components.Health;
using PixelCrew.Creatures.Core;
using PixelCrew.Model;
using UnityEngine;

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

            _coinScore.Init(_session);
            _jumper.Init(Animator, Rigidbody);
            _thrower.Init(Animator, _session);
            Movement.Init(Animator, Rigidbody);
            _health.SetOriginHealth(_session.Data.Health);

            var heroAttacker = Attacker as HeroAttacker;

            if (heroAttacker != null)
            {
                heroAttacker.Init(Animator, _session);
                _reader.Init(Movement, _jumper, _interact, heroAttacker, _thrower);
            }
        }

        public void OnHealthChanged(int value)
        {
            _session.Data.Health = value;
        }
    }
}