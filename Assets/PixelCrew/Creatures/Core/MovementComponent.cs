using System;
using System.Collections;
using UnityEngine;

namespace PixelCrew.Creatures.Core
{
    public class MovementComponent : MonoBehaviour
    {
        [SerializeField] private float _speed = 3f;

        private float _direction;
        private Rigidbody2D _rigidbody;
        private Animator _animator;
        private float _speedUpValue;

        private const float DiedZone = 0.01f;

        public bool IsSpeedUpWork { get; private set; }

        private void FixedUpdate()
        {
            if (Mathf.Abs(_direction) < DiedZone)
                _direction = 0f;

            float tempDirection = _direction switch
            {
                > 0 => 1,
                < 0 => -1,
                _ => 0
            };

            _rigidbody.velocity = new Vector2(tempDirection * _speed, _rigidbody.velocity.y);
            _animator.SetBool(CreatureAnimations.IsRunning, Mathf.Abs(_direction) > DiedZone);
        }

        public void Init(Animator animator, Rigidbody2D rigidbody)
        {
            _animator = animator;
            _rigidbody = rigidbody;
        }

        public void SetDirection(float direction)
        {
            _direction = direction;
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

        public IEnumerator SpeedUp(float value, float duration, Action callback = null)
        {
            IsSpeedUpWork = true;
            _speedUpValue = value;
            _speed += _speedUpValue;
            yield return new WaitForSeconds(duration);
            _speed -= _speedUpValue;
            _speedUpValue = 0;
            IsSpeedUpWork = false;
            callback?.Invoke();
        }

        public void SetSpeed(float speed)
        {
            if (IsSpeedUpWork == true)
            {
                _speed = _speedUpValue + speed;
            }
            else
            {
                _speed = speed;
            }
        }
    }
}