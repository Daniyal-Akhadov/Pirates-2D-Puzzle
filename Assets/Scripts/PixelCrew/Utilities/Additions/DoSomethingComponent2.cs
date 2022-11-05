using UnityEngine;
using UnityEngine.Events;

namespace PixelCrew.Utilities.Additions
{
    public class DoSomethingComponent2 : MonoBehaviour
    {
        [SerializeField] private UnityEvent _action;

        public void Do2()
        {
            _action?.Invoke();
        }
    }
}