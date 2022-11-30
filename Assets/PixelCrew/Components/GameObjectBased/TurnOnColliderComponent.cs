using UnityEngine;

namespace PixelCrew.Components.GameObjectBased
{
    public class TurnOnColliderComponent : MonoBehaviour
    {
        [SerializeField] private float _delay = 0.2f;
        [SerializeField] private Collider2D _collider;

        private void Start()
        {
            Invoke(nameof(TurnOn), _delay);
        }

        private void TurnOn()
        {
            _collider.enabled = true;
        }
    }
}