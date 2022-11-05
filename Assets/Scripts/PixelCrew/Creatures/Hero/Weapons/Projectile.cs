using UnityEngine;

namespace PixelCrew.Creatures.Hero.Weapons
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private float _speed = 2f;

        private Rigidbody2D _rigidbody;
        private float _direction;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            _direction = transform.localScale.x;
        }

        private void FixedUpdate()
        {
            Vector3 position = _rigidbody.position;
            position.x += _speed * _direction * Time.fixedDeltaTime;
            _rigidbody.MovePosition(position);
        }
    }
}