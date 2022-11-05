using PixelCrew.Creatures.Hero;
using UnityEngine;

namespace PixelCrew.Loots
{
    public class ArmHeroComponent : MonoBehaviour
    {
        public void Arm(GameObject hero)
        {
            if (hero.TryGetComponent(out HeroAttacker attacker))
            {
                attacker.Arm();
            }
        }
    }
}