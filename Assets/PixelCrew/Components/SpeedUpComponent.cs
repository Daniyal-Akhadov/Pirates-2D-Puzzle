using PixelCrew.Creatures.Core;
using UnityEngine;

namespace PixelCrew.Components
{
    public class SpeedUpComponent : MonoBehaviour
    {
        [Range(1, 5)] [SerializeField] private float _multiplier = 2f;
        [SerializeField] private float _duration;

        public bool IsWork { get; private set; }

        public void Do(GameObject target)
        {
            var movement = target.GetComponent<MovementComponent>();

            if (movement != null)
            {
                IsWork = true;
                StartCoroutine(movement.SpeedUp(_multiplier, _duration, () => IsWork = false));
            }
        }
    }
}