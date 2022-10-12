using System;
using UnityEngine;
using UnityEngine.Events;

namespace PixelCrew.Hero
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(HeroCoinScore))]
    public class HeroHealth : MonoBehaviour, IDamageable
    {
        [SerializeField] private int _health = 5;
        [SerializeField] private float _pushAwayForce = 60f;

        [SerializeField] private UnityEvent _onDamaged;
        [SerializeField] private UnityEvent _onDied;

        private Animator _animator;
        private Rigidbody2D _rigidbody;
        private HeroCoinScore _coinScore;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _coinScore = GetComponent<HeroCoinScore>();
            _animator = GetComponentInChildren<Animator>();
        }

        private void PushAway()
        {
            Vector2 velocity = _rigidbody.velocity;
            velocity.y = 0f;
            _rigidbody.velocity = new Vector2(velocity.x, _pushAwayForce);
        }

        public void ApplyDamage(int value)
        {
            if (value <= 0)
                throw new ArgumentException($"Incorrect argument: {value} <= 0!");

            _health -= value;
            PushAway();
            _coinScore.TrySpawnCoinExplosion();

            _onDamaged?.Invoke();
            _animator.SetTrigger(HeroAnimations.Hit);

            if (_health <= 0)
            {
                _onDied?.Invoke();
            }
        }

        public void AddHealth(int value)
        {
            if (value <= 0)
                throw new ArgumentException($"Incorrect argument: {value} <= 0!");

            _health += value;
        }
    }
}