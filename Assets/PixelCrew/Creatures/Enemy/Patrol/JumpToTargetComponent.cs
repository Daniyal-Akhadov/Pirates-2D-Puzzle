using System;
using PixelCrew.Utilities.TimeManagement;
using UnityEngine;

namespace PixelCrew.Creatures.Enemy.Patrol
{
    [Serializable]
    public class JumpToTargetComponent
    {
        [SerializeField] private Vector2 _difference = Vector2.one;
        [SerializeField] private float _force = 240f;
        [SerializeField] private Cooldown _cooldown;

        private Rigidbody2D _rigidbody;
        private Transform _target;

        public Vector2 Difference => _difference;

        public void Init(Rigidbody2D rigidbody, Transform target)
        {
            _rigidbody = rigidbody;
            _target = target;
        }

        public void TryJump()
        {
            Vector2 distance = (Vector2)_target.position - _rigidbody.position;
            bool isNear = (Mathf.Abs(distance.x) <= _difference.x && Mathf.Abs(distance.y) <= _difference.y);
            
            if (isNear && _cooldown.IsReady == true)
            {
                _rigidbody.AddForce(Vector2.up * _force, ForceMode2D.Impulse);
                _cooldown.Reset();
            }
        }
    }
}