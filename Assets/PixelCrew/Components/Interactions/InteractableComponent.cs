using UnityEngine;
using UnityEngine.Events;

namespace PixelCrew.Components.Interactions
{
    public class InteractableComponent : MonoBehaviour
    {
        [SerializeField] private UnityEvent _onInteract;

        public void Interact()
        {
            print("Interact");
            _onInteract?.Invoke();
        }
    }
}