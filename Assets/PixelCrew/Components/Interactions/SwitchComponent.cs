using System;
using UnityEngine;

namespace PixelCrew.Components.Interactions
{
    public class SwitchComponent : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private bool _state = true;
        [SerializeField] private string _animationKey;
        [SerializeField] private bool _updateOnStart;

        private void Start()
        {
            if (_updateOnStart == true)
                Switch();
        }

        public void Switch()
        {
            _animator.SetBool(_animationKey, _state);
            _state = _state == false;
        }
    }
}