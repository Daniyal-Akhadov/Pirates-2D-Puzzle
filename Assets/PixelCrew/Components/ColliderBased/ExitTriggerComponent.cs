using PixelCrew.Utilities;
using UnityEngine;
using UnityEngine.Events;

namespace PixelCrew.Components.ColliderBased
{
    public class ExitTriggerComponent : MonoBehaviour
    {
        [SerializeField] private string _tag;
        [SerializeField] private LayerMask _layer = ~0;
        [SerializeField] private UnityEvent _action;
        [SerializeField] private EnterEvent _actionWithArgument;

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.IsLayerIn(_layer) == false)
                return;

            if (string.IsNullOrEmpty(_tag) == false && other.CompareTag(_tag) == false)
                return;

            _action?.Invoke();
            _actionWithArgument?.Invoke(other.gameObject);
        }
    }
}