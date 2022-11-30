using System;
using System.Collections;
using UnityEngine;

namespace PixelCrew.Creatures.Enemy.Patrol
{
    public class PlatformPatrol : Patrol
    {
        [SerializeField] private Vector2 _rayDirection;
        [SerializeField] private Transform _platform;
        [SerializeField] private JumpToTargetComponent _jumpToTarget;
      
        private int _direction;
        private bool _isOnPlatform;
        private Rigidbody2D _rigidbody;

        private const int RayDistance = 1;

        public bool OnPlatform => Physics2D.Raycast(transform.position, _rayDirection, RayDistance, (int)Mathf.Pow(2, LayerMask.NameToLayer("Platform")));

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            _jumpToTarget.Init(_rigidbody, _platform);
        }

        public override IEnumerator DoPatrol()
        {
            var waitForSeconds = new WaitForSeconds(StopTime);
            DefineCurrentDirection();

            while (enabled == true)
            {
                if (OnPlatform == false)
                {
                    Movement.Stop();
                    yield return waitForSeconds;
                    DefineCurrentDirection();
                }

                yield return null;
            }
        }

        public IEnumerator ReturnToPlatform(Action callback)
        {
            var waitForSeconds = new WaitForSeconds(StopTime);
            Vector2 distance;

            yield return waitForSeconds;

            do
            {
                distance = _platform.position - transform.position;
                Movement.SetDirection(distance.normalized.x);
                yield return null;
            } while (Mathf.Abs(distance.x) > _jumpToTarget.Difference.x);

            _jumpToTarget.TryJump();
            callback?.Invoke();
        }

        private void DefineCurrentDirection()
        {
            _direction = (int)(_direction == (int)Direction.Left ? Direction.Right : Direction.Left);
            float rayDirectionX = _rayDirection.x;
            rayDirectionX = Mathf.Abs(rayDirectionX) * _direction;
            _rayDirection.x = rayDirectionX;

            Movement.SetDirection(_direction);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawRay(transform.position, _rayDirection * RayDistance);
        }

        private enum Direction
        {
            Right = 1,
            Left = -1
        }
    }
}