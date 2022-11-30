using System.Collections;
using UnityEngine;

namespace PixelCrew.Creatures.Enemy.Patrol
{
    public class PointsPatrol : Patrol
    {
        [SerializeField] private Transform[] _points;
        [SerializeField] private float _threshold = 0.5f;

        private int _destinationPointIndex;

        private Vector2 Direction => _points[_destinationPointIndex].position - transform.position;
        private bool IsOnPoint => Direction.magnitude < _threshold;

        private void Awake()
        {
            foreach (var point in _points)
            {
                point.parent = null;
            }
        }

        public override IEnumerator DoPatrol()
        {
            while (enabled == true)
            {
                Movement.SetDirection(Direction.normalized.x);

                if (IsOnPoint == true)
                {
                    _destinationPointIndex = (int)Mathf.Repeat(_destinationPointIndex + 1, _points.Length);
                    Movement.Stop();
                    yield return new WaitForSeconds(StopTime);
                }

                yield return null;
            }
        }
    }
}