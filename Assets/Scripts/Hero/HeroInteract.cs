using PixelCrew.Components;
using UnityEngine;

namespace PixelCrew
{
    public class HeroInteract : MonoBehaviour
    {
        [SerializeField] private LayerMask _layer;
        [SerializeField] private float _radius = 1f;

        private readonly Collider2D[] _result = new Collider2D[1];

        public void Interact()
        {
            int size = Physics2D.OverlapCircleNonAlloc(transform.position, _radius, _result, _layer);

            for (var i = 0; i < size; i++)
            {
                var result = _result[i];
                var interactable = result.GetComponent<InteractableComponent>();

                if (interactable != null)
                    interactable.Interact();
            }
        }
    }
}