using System;
using PixelCrew.Components;
using UnityEngine;
using UnityEngine.Events;

namespace PixelCrew
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class HeroJumper : MonoBehaviour
    {
        [SerializeField] private float _force = 50f;
        [SerializeField] private SpawnComponent _fallDustSpawner;
        [SerializeField] private UnityEvent _onJumped;

        private Rigidbody2D _rigidbody;
        private Animator _animator;
        private bool _isGround;
        private bool _isJumpPress;
        private bool _isJumping;
        private bool _isDoubleJumpAvailable;
        private int _currentClick;

        private const float AvailableAngleForJumping = 45f;
        private const float StopJumpingMultiplier = 0.5f;
        private const int MaxJumpCount = 2;
        private const float VerticalVelocityToShowFallDust = -5f;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _animator = GetComponentInChildren<Animator>();
        }

        private void FixedUpdate()
        {
            Vector2 velocity = _rigidbody.velocity;

            if (_isJumpPress == true)
            {
                bool isFall = velocity.y <= 0.02f;
                bool isFirstJump = _isGround == true && isFall == true;
                bool isSecondJump = _isDoubleJumpAvailable == true && isFall == true;

                switch (_currentClick)
                {
                    case 1 when isFirstJump == true:
                        AddForce();
                        _isGround = false;
                        break;
                    case 2 when isSecondJump == true:
                        AddForce();
                        _isDoubleJumpAvailable = false;
                        break;
                }
            }
            else
            {
                if (velocity.y > 0f && _isJumping == true)
                {
                    _rigidbody.velocity = new Vector2(velocity.x, velocity.y * StopJumpingMultiplier);
                }
            }

            _animator.SetFloat(HeroAnimations.VerticalVelocity, _rigidbody.velocity.y);
            _animator.SetBool(HeroAnimations.IsGround, _isGround);
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            var contacts = col.contacts;

            Vector2 normal = contacts[0].normal;

            if (Vector3.Angle(Vector3.up, normal) < AvailableAngleForJumping)
            {
                if (_rigidbody.velocity.y < VerticalVelocityToShowFallDust || _currentClick >= MaxJumpCount)
                {
                    _fallDustSpawner.Spawn();
                }
            }
        }

        private void OnCollisionStay2D(Collision2D col)
        {
            var contacts = col.contacts;

            foreach (var contact in contacts)
            {
                Vector2 normal = contact.normal;

                if (Vector3.Angle(Vector3.up, normal) < AvailableAngleForJumping)
                {
                    _isJumping = false;
                    _isDoubleJumpAvailable = true;
                    _currentClick = 0;
                    _isGround = true;
                    return;
                }
            }
        }

        private void OnCollisionExit2D(Collision2D _)
        {
            _isGround = false;
        }

        private void AddForce()
        {
            _onJumped?.Invoke();
            _isJumping = true;
            _rigidbody.velocity += Vector2.up * _force;
        }

        public void Jump(bool pressed)
        {
            _isJumpPress = pressed;
            _currentClick++;
        }

        public void StopJumping()
        {
            _isJumping = false;
        }
    }
}