using PixelCrew.Utilities;
using UnityEngine;

namespace PixelCrew.Creatures.Hero.Weapons
{
    public abstract class Projectile : MonoBehaviour
    {
        [SerializeField] private float _speed = 2f;

        protected Rigidbody2D Rigidbody;
        protected float Direction;

        public SpriteAnimation SpriteAnimation;

        protected float Speed => _speed;

        private void Awake()
        {
            Rigidbody = GetComponent<Rigidbody2D>();
            SpriteAnimation = GetComponentInChildren<SpriteAnimation>();
        }

        private void Start()
        {
            Direction = transform.localScale.x;
        }

        protected Vector3 CalculateX()
        {
            Vector3 position = Rigidbody.position;
            position.x += Speed * Direction * Time.fixedDeltaTime;
            return position;
        }
    }
}