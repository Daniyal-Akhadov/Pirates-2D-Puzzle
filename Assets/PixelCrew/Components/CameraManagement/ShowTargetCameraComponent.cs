using UnityEngine;

namespace PixelCrew.Components.CameraManagement
{
    public class ShowTargetCameraComponent : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        [SerializeField] private ShowTargetCameraStateController _controller;

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
        }

        public void MoveBack()
        {
            _controller.SetState(false);
        }
    }
}