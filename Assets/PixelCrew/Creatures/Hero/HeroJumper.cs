using System.Linq;
using PixelCrew.Components.GameObjectBased;
using PixelCrew.Creatures.Core;
using PixelCrew.Model;
using PixelCrew.Utilities;
using UnityEngine;
using UnityEngine.Events;

namespace PixelCrew.Creatures.Hero
{
    public class HeroJumper : MonoBehaviour
    {
        [SerializeField] private LayerMask _groundLayer;
        [SerializeField] private float _force = 50f;
        [SerializeField] private SpawnComponent _fallDustSpawner;
        [SerializeField] private Transform _rayPoint;
        [SerializeField] private float _rayDistance = 0.5f;
        [SerializeField] private UnityEvent _onJumped;

        private GameSession _session;
        private Rigidbody2D _rigidbody;
        private Animator _animator;
        private bool _isGround;
        private bool _isJumpPress;
        private bool _isDoubleJumpAvailable;
        private int _currentClick;
        private bool _isJumpingStarted;

        private const float AvailableAngleForJumping = 45f;
        private const float StopJumpingMultiplier = 0.8f;
        private const float RelativeVelocityToShowFallDust = 15f;

        public bool IsJumping { get; private set; }

        private void FixedUpdate()
        {
            Vector2 velocity = _rigidbody.velocity;

            if (_isJumpPress == true)
            {
                bool isFall = velocity.y <= 0.05f;
                bool isFirstJump = _isGround == true && isFall == true;
                bool isSecondJump = _isDoubleJumpAvailable == true
                                    && isFall == true;

                switch (_currentClick)
                {
                    case 1 when isFirstJump == true:
                        _isJumpingStarted = true;
                        AddForce();
                        _isGround = false;
                        break;
                    case 2 when isSecondJump == true && _session.PerksModel.IsDoubleJumpSupported:
                        AddForce();
                        _isDoubleJumpAvailable = false;
                        break;
                    default:
                        _isJumpingStarted = false;
                        break;
                }
            }
            else
            {
                if (velocity.y > 0f && IsJumping == true)
                {
                    _rigidbody.velocity = new Vector2(velocity.x, velocity.y * StopJumpingMultiplier);
                }
            }

            _animator.SetFloat(CreatureAnimations.VerticalVelocity, _rigidbody.velocity.y);
            _animator.SetBool(CreatureAnimations.IsGround, _isGround);
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            if (col.gameObject.IsLayerIn(_groundLayer))
            {
                var contact = col.contacts[0];

                if (Vector3.Angle(Vector3.up, contact.normal) < AvailableAngleForJumping)
                {
                    if (contact.relativeVelocity.y >= RelativeVelocityToShowFallDust)
                    {
                        _fallDustSpawner.Spawn();
                    }
                }
            }
        }

        private void OnCollisionStay2D(Collision2D col)
        {
            const string PlatformLayer = "Platform";
            bool isPlatform = col.gameObject.IsLayerIn((int)Mathf.Pow(2, LayerMask.NameToLayer(PlatformLayer)));
            var contacts = col.contacts;

            if (isPlatform == false && _isJumpingStarted == false &&
                contacts.Select(contact => contact.normal)
                    .Any(normal => Vector3.Angle(Vector3.up, normal) < AvailableAngleForJumping))
            {
                StandOnThePlane();
            }
            else if (isPlatform == true && _isJumpingStarted == false)
            {
                var hit = Physics2D.Raycast(
                    _rayPoint.position,
                    Vector3.down,
                    _rayDistance,
                    (int)Mathf.Pow(2, LayerMask.NameToLayer(PlatformLayer)));

                if (hit == true && _rigidbody.velocity.y <= 0.5f)
                {
                    StandOnThePlane();
                }
            }
        }

        private void StandOnThePlane()
        {
            IsJumping = false;
            _isDoubleJumpAvailable = true;
            _currentClick = 0;
            _isGround = true;
        }

        private void OnCollisionExit2D(Collision2D _)
        {
            _isGround = false;
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawRay(_rayPoint.position, Vector3.down * _rayDistance);
        }

        public void Init(Animator animator, Rigidbody2D rigidbody, GameSession session)
        {
            _animator = animator;
            _rigidbody = rigidbody;
            _session = session;
        }

        public void Jump(bool pressed)
        {
            _isJumpPress = pressed;
        }

        public void Press()
        {
            _currentClick += 1;
        }

        public void StopJumping()
        {
            IsJumping = false;
        }

        private void AddForce()
        {
            _onJumped?.Invoke();
            IsJumping = true;
            _rigidbody.velocity += Vector2.up * _force;
        }
    }
}