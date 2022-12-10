using UnityEngine;
using UnityEngine.Events;

namespace PixelCrew.Components.CameraManagement
{
    public class ShowTargetCameraComponent : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        [SerializeField] private ShowTargetCameraStateController _controller;
        [SerializeField] private float _delay;
        [SerializeField] private UnityEvent _onDelay;

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (_controller == null)
                _controller = FindObjectOfType<ShowTargetCameraStateController>();
        }
#endif

        public void Show()
        {
            _controller.SetPosition(_target.position);
            _controller.SetState(true);
            Invoke(nameof(InvokeDelayEvent), _delay);
        }

        public void MoveBack()
        {
            _controller.SetState(false);
        }

        private void InvokeDelayEvent()
        {
            _onDelay?.Invoke();
        }
    }
}