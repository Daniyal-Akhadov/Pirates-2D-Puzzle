using UnityEngine;

namespace PixelCrew.Components
{
    public class SwitchComponent : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private string _animationKey;

        private bool _state = true;

        public void Switch()
        {
            _animator.SetBool(_animationKey, _state);
            _state = _state == false;
        }
    }
}