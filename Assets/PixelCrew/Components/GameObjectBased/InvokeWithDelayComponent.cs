using UnityEngine;
using UnityEngine.Events;

namespace PixelCrew.Components.GameObjectBased
{
    public class InvokeWithDelayComponent : MonoBehaviour
    {
        [SerializeField] private float _delay = 1f;
        [SerializeField] private UnityEvent _onInvoke;

        private void Start()
        {
            Invoke(nameof(Do), _delay);
        }

        private void Do()
        {
            _onInvoke?.Invoke();
        }
    }
}