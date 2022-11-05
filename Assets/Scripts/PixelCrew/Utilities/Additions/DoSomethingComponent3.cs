using UnityEngine;
using UnityEngine.Events;

namespace PixelCrew.Utilities.Additions
{
    public class DoSomethingComponent3 : MonoBehaviour
    {
        [SerializeField] private UnityEvent _action;

        public void Do3()
        {
            _action?.Invoke();
        }
    }
}