using UnityEngine;

namespace PixelCrew.Creatures.Hero.Weapons
{
    public class LinearProjectile : Projectile
    {
        private void FixedUpdate()
        {
            Vector3 position = CalculateX();
            Rigidbody.MovePosition(position);
        }
    }
}