using System;
using System.Collections;
using System.Linq;
using PixelCrew.Components.ColliderBased;
using PixelCrew.Components.GameObjectBased;
using PixelCrew.Creatures.Core;
using PixelCrew.Creatures.Enemy.Patrol;
using PixelCrew.Creatures.Hero;
using UnityEngine;

namespace PixelCrew.Creatures.Enemy
{
    public class Sharky : MobAI
    {
        [Header("Follow")] [SerializeField] private JumpToTargetComponent _jumpToTarget;
        [SerializeField] private float _timeToFollowAfterLeavingZone = 1f;
        [SerializeField] private float _emotionDelay = 0.7f;
        [SerializeField] private LayerCheck _visionRange;
        [SerializeField] private SpawnListComponent _emotions;

        private Vector2 DirectionToTarget => (_target.transform.position - transform.position).normalized;

        private const float CheckUpOnPlayer = 0.8f;
        private Hero.Hero _target;
        private bool _isFollowing;
        private Collider2D _collider;
        private bool _isGround;

        private const float AvailableAngleForJumping = 45f;

        protected override void Awake()
        {
            base.Awake();
            _collider = GetComponent<Collider2D>();
        }

        private void FixedUpdate()
        {
            Animator.SetFloat(CreatureAnimations.VerticalVelocity, Rigidbody.velocity.y);
            Animator.SetBool(CreatureAnimations.IsGround, _isGround);
        }

        private void OnCollisionStay2D(Collision2D col)
        {
            var contacts = col.contacts;

            if (contacts.Select(contact => contact.normal)
                .Any(normal => Vector3.Angle(Vector3.up, normal) < AvailableAngleForJumping))
            {
                _isGround = true;
            }
        }

        private void OnCollisionExit2D(Collision2D other)
        {
            _isGround = false;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position + Vector3.up * CheckUpOnPlayer, CheckUpOnPlayer);
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position - Vector3.up * CheckUpOnPlayer, CheckUpOnPlayer);
        }

        public void OnHeroInVision(GameObject target)
        {
            if (_isFollowing == true)
                return;

            _target = target.GetComponent<Hero.Hero>();
            _jumpToTarget.Init(Rigidbody, _target.transform);
            StartState(Exclaim());
        }

        protected override IEnumerator Attack()
        {
            var waitForSeconds = new WaitForSeconds(AttackCooldown);

            while (_attackRange.IsTouchingLayer == true)
            {
                Attacker.Attack();
                yield return waitForSeconds;
            }

            yield return new WaitForSeconds(0.5f);
            StartState(GoToHero());
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
            float followTimer = _timeToFollowAfterLeavingZone;
            _isFollowing = true;

            while (_visionRange.IsTouchingLayer == true || followTimer > 0f)
            {
                if (_visionRange.IsTouchingLayer == true)
                    followTimer = _timeToFollowAfterLeavingZone;
                else
                    followTimer -= Time.deltaTime;

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
            _isFollowing = false;
            _emotions.Spawn("Miss");
            yield return new WaitForSeconds(_emotionDelay);

            var platformPatrol = Patrol as PlatformPatrol;
            StartState(platformPatrol != null && platformPatrol.OnPlatform == false
                ? platformPatrol.ReturnToPlatform(() => StartState(Patrol.DoPatrol()))
                : Patrol.DoPatrol());
        }

        private void SetDirectionToTarget()
        {
            int player = (int)Mathf.Pow(2, LayerMask.NameToLayer("Player"));
            var onHead = Physics2D.Raycast(transform.position, Vector2.up, 0.5f, player);

            if (onHead == false)
                Movement.SetDirection(DirectionToTarget.x);

            if (onHead == true)
            {
                _jumpToTarget.TryJump();
            }
            else if (_target.OnPlatform == true && OnPlatform == false)
            {
                var hit = Physics2D.CircleCast(transform.position + Vector3.up * CheckUpOnPlayer,
                    CheckUpOnPlayer,
                    Vector3.forward,
                    1f, player);

                float distanceX = _target.transform.position.x - transform.position.x;

                if (hit == true && Mathf.Abs(distanceX) < _jumpToTarget.Difference.x)
                {
                    _jumpToTarget.TryJump();
                }
            }
            else if (_target.OnPlatform == false && OnPlatform == true)
            {
                var hit = Physics2D.CircleCast(transform.position - Vector3.up * CheckUpOnPlayer,
                    CheckUpOnPlayer,
                    Vector3.forward,
                    1f, player);

                if (_target.TryGetComponent(out HeroJumper jumper))
                {
                    if (jumper.IsJumping == false)
                    {
                        float distanceX = _target.transform.position.x - transform.position.x;

                        if (hit == true && Mathf.Abs(distanceX) < _jumpToTarget.Difference.x)
                        {
                            TurnOffCollider();
                            Invoke(nameof(TurnOnCollider), 0.1f);
                        }
                    }
                }
            }
        }

        private void TurnOnCollider()
        {
            _collider.enabled = true;
        }

        private void TurnOffCollider()
        {
            _collider.enabled = false;
        }
    }
}