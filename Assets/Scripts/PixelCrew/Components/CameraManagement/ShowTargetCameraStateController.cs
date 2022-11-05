using Cinemachine;
using UnityEngine;

namespace PixelCrew.Components.CameraManagement
{
    public class ShowTargetCameraStateController : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private CinemachineVirtualCamera _showCamera;

        private static readonly int ShowTarget = Animator.StringToHash("show_target");

        public void SetPosition(Vector2 position)
        {
            float positionZ = _showCamera.transform.position.z;
            Vector3 targetPosition = position;
            targetPosition.z = positionZ;
            _showCamera.transform.position = targetPosition;
        }

        public void SetState(bool show)
        {
            _animator.SetBool(ShowTarget, show);
        }
    }
}