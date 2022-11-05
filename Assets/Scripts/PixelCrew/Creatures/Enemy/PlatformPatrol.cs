using System.Collections;
using UnityEngine;

namespace PixelCrew.Creatures.Enemy
{
    public class PlatformPatrol : Patrol
    {
        [SerializeField] private Vector2 _rayDirection;
        [SerializeField] private LayerMask _layer;

        private int _direction;
        private bool _isOnPlatform;

        private const int RayDistance = 1;

        public override IEnumerator DoPatrol()
        {
            var waitForSeconds = new WaitForSeconds(StopTime);
            DefineCurrentDirection();

            while (enabled == true)
            {
                var hit = Physics2D.Raycast(transform.position, _rayDirection, RayDistance, _layer);

                if (hit == false)
                {
                    Movement.Stop();
                    yield return waitForSeconds;
                    DefineCurrentDirection();
                }

                yield return null;
            }
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