using UnityEngine;
using UnityEngine.Events;

namespace PixelCrew.Components
{
    public class DoSomethingComponent : MonoBehaviour
    {
        [SerializeField] private UnityEvent _action;

        public void Do()
        {
            _action?.Invoke();
        }
    }
}