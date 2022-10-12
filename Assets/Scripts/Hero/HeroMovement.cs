using UnityEngine;

namespace PixelCrew
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class HeroMovement : MonoBehaviour
    {
        [SerializeField] private float _speed = 3f;

        private float _direction;
        private Rigidbody2D _rigidbody;
        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponentInChildren<Animator>();
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            _rigidbody.velocity = new Vector2(_direction * _speed, _rigidbody.velocity.y);
        }

        public void SetDirection(float direction)
        {
            _direction = direction;
            _animator.SetBool(HeroAnimations.IsRunning, _direction != 0f);
            FlipBy(direction);
        }

        private void FlipBy(float direction)
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
    }
}