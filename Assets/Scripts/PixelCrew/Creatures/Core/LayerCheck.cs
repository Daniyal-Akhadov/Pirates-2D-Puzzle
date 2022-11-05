using UnityEngine;

namespace PixelCrew.Creatures.Core
{
    public class LayerCheck : MonoBehaviour
    {
        [SerializeField] private LayerMask _layer;
        [SerializeField] private Collider2D _collider;
        [SerializeField] private bool _isTouchingLayer;
        public bool IsTouchingLayer => _isTouchingLayer;

        private void OnTriggerStay2D(Collider2D other)
        {
            _isTouchingLayer = _collider.IsTouchingLayers(_layer);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            _isTouchingLayer = _collider.IsTouchingLayers(_layer);
        }
    }
}