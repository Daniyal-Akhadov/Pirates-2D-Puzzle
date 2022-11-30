using System.Collections;
using PixelCrew.Components.ColliderBased;
using PixelCrew.Creatures.Core;
using UnityEngine;

namespace PixelCrew.Creatures.Enemy
{
    public abstract class MobAI : Creature
    {
        [SerializeField] protected Patrol.Patrol Patrol;
        [SerializeField] protected float AttackCooldown;
        [SerializeField] protected LayerCheck _attackRange;

        private IEnumerator _currentCoroutine;
        private bool _isDead;

        protected override void Awake()
        {
            base.Awake();
            Patrol.Init(Movement);
        }

        private void Start()
        {
            StartState(Patrol.DoPatrol());
        }

        public void OnDie()
        {
            _isDead = true;
            Animator.SetBool(CreatureAnimations.IsDead, _isDead);
            Movement.Stop();

            if (_currentCoroutine != null)
            {
                StopCoroutine(_currentCoroutine);
            }
        }

        protected abstract IEnumerator Attack();

        protected void StartState(IEnumerator coroutine)
        {
            if (_isDead == true)
                return;

            Movement.Stop();

            if (_currentCoroutine != null)
            {
                StopCoroutine(_currentCoroutine);
            }

            _currentCoroutine = coroutine;
            StartCoroutine(_currentCoroutine);
        }
    }
}