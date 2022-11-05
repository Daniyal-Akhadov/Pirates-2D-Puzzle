using UnityEngine;

namespace PixelCrew.Components.Interactions
{
    public class DoInteractComponent : MonoBehaviour
    {
        public void DoInteract(GameObject element)
        {
            if (element.TryGetComponent(out InteractableComponent component))
            {
                component.Interact();
            }
        }
    }
}