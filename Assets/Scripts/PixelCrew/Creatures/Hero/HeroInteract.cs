using PixelCrew.Components.ColliderBased;
using UnityEngine;

namespace PixelCrew.Creatures.Hero
{
    public class HeroInteract : MonoBehaviour
    {
        [SerializeField] private CheckCircleOverlapComponent _range;

        public void Interact()
        {
            _range.Check();
        }
    }
}