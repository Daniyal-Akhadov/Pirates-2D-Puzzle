using System;
using UnityEngine;

namespace PixelCrew.Utilities
{
    public class SetRendererToBackground : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _renderer;
        [SerializeField] private Color _targetColor = UsefulColors.TrashColor;
        [SerializeField] private bool _onAwake;

        private const string Background = "Background";

        private void Awake()
        {
            if (_onAwake == true)
            {
                Set();
            }
        }

        public void Set()
        {
            _renderer.color = _targetColor;
            _renderer.sortingLayerName = Background;
        }
    }
}