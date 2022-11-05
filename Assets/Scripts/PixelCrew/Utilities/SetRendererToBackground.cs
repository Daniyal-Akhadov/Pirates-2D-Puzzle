using UnityEngine;

namespace PixelCrew.Utilities
{
    public class SetRendererToBackground : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _renderer;
        [SerializeField] private Color _targetColor = UsefulColors.TrashColor;

        private const string Background = "Background";

        public void Set()
        {
            _renderer.color = _targetColor;
            _renderer.sortingLayerName = Background;
        }
    }
}