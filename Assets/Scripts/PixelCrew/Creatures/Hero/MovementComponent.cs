using PixelCrew.Creatures.Core;
using UnityEngine;

namespace PixelCrew.Creatures.Hero
{
    public class MovementComponent : MonoBehaviour
    {
        [SerializeField] private float _speed = 3f;

        private float _direction;
        private Rigidbody2D _rigidbody;
        private Animator _animator;

        private void FixedUpdate()
        {
            _rigidbody.velocity = new Vector2(_direction * _speed, _rigidbody.velocity.y);
        }

        public void Init(Animator animator, Rigidbody2D rigidbody)
        {
            _animator = animator;
            _rigidbody = rigidbody;
        }

        public void SetDirection(float direction)
        {
            _direction = direction;
            _animator.SetBool(CreatureAnimations.IsRunning, _direction != 0f);
            FlipBy(direction);
        }

        public void FlipBy(float direction)
        {
            Vector3 targetScale = transform.localScale;

            targetScale.x = direction switch
            {
                > 0f => 1,
                < 0f => -1,
                _ => targetScale.x
            };

            transform.localScale = targetScale;
        }

        public void Stop()
        {
            _direction = Vector2.zero.x;
            _animator.SetBool(CreatureAnimations.IsRunning, _direction != 0f);
        }
    }
}