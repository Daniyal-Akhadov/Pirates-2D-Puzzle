using UnityEngine;

namespace PixelCrew.Creatures.Hero.Weapons
{
    public class SinusoidalProjectile : Projectile
    {
        [SerializeField] private float _frequency = 1f;
        [SerializeField] private float _amplitude = 1f;
        private float _time;

        private void FixedUpdate()
        {
            Vector3 position = CalculateX();
            position.y += Mathf.Sin(_time * _frequency) * _amplitude;
            Rigidbody.MovePosition(position);
            _time += Time.fixedDeltaTime;
        }
    }
}