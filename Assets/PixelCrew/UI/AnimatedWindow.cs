using System;
using UnityEngine;

namespace PixelCrew.UI
{
    public class AnimatedWindow : MonoBehaviour
    {
        private Animator _animator;

        private static readonly int ShowTag = Animator.StringToHash("Show");
        private static readonly int HideTag = Animator.StringToHash("Hide");

        protected virtual void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        protected virtual void Start()
        {
            Show();
        }

        protected void Show()
        {
            _animator.SetTrigger(ShowTag);
        }

        public void Close()
        {
            _animator.SetTrigger(HideTag);
        }

        public virtual void OnCloseAnimationComplete()
        {
            Destroy(gameObject);
        }
    }
}