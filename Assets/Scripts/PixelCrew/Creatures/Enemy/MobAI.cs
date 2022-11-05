using System.Collections;
using PixelCrew.Components.GameObjectBased;
using PixelCrew.Creatures.Core;
using UnityEngine;

namespace PixelCrew.Creatures.Enemy
{
    public class MobAI : Creature
    {
        [SerializeField] private SpawnListComponent _emotions;
        [SerializeField] private Patrol _patrol;

        [Header("Time")] [SerializeField] private float _emotionDelay = 0.7f;
        [SerializeField] private float _attackCooldown;

        [Header("Ranges")] [SerializeField] private LayerCheck _visionRange;
        [SerializeField] private LayerCheck _attackRange;

        private GameObject _target;
        private IEnumerator _currentCoroutine;
        private bool _isDead;

        private Vector2 DirectionToTarget => (_target.transform.position - transform.position).normalized;

        protected override void Awake()
        {
            base.Awake();
            _patrol.Init(Movement);
        }

        private void Start()
        {
            StartState(_patrol.DoPatrol());
        }

        public void OnHeroInVision(GameObject target)
        {
            _target = target;
            StartState(Exclaim());
        }

        public void OnDie()
        {
            _isDead = true;
            Movement.Stop();
            Animator.SetBool(CreatureAnimations.IsDead, _isDead);

            if (_currentCoroutine != null)
            {
                StopCoroutine(_currentCoroutine);
            }
        }

        private IEnumerator Exclaim()
        {
            Movement.Stop();
            Movement.FlipBy(DirectionToTarget.x);
            _emotions.Spawn("Exclamation");
            yield return new WaitForSeconds(_emotionDelay);
            StartState(GoToHero());
        }

        private IEnumerator GoToHero()
        {
            while (_visionRange.IsTouchingLayer == true)
            {
                if (_attackRange.IsTouchingLayer == true)
                {
                    StartState(Attack());
                }
                else
                {
                    SetDirectionToTarget();
                }

                yield return null;
            }

            Movement.Stop();
            _emotions.Spawn("Miss");
            yield return new WaitForSeconds(_emotionDelay);
            StartState(_patrol.DoPatrol());
        }

        private IEnumerator Attack()
        {
            var waitForSeconds = new WaitForSeconds(_attackCooldown);

            while (_attackRange.IsTouchingLayer == true)
            {
                Attacker.Attack();
                yield return waitForSeconds;
            }

            StartState(GoToHero());
        }

        private void StartState(IEnumerator coroutine)
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

        private void SetDirectionToTarget()
        {
            Movement.SetDirection(DirectionToTarget.x);
        }
    }
}