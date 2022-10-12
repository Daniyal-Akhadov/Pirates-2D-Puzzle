using UnityEngine;
using UnityEngine.Events;

namespace PixelCrew.Components
{
    public class EnterTriggerComponent : MonoBehaviour
    {
        [SerializeField] private string _tag;
        [SerializeField] private UnityEvent _action;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag(_tag))
            {
                _action?.Invoke();
            }
        }
    }
}