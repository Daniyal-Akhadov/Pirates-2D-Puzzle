using UnityEngine;

namespace PixelCrew.Components.Effects
{
    public class ParallaxEffect : MonoBehaviour
    {
        [Range(0, 4)] [SerializeField] private float _effectValue = 0.5f;
        [Range(0, 0.1f)] [SerializeField] private float _defaultEffectValue = 0.05f;
        [SerializeField] private Transform _followTarget;
        [SerializeField] private bool _isInverse;

        private float _startPositionX;
        private Vector3 _defaultPosition;

        private void Start()
        {
            _startPositionX = transform.position.x;
        }

        private void LateUpdate()
        {
            var position = transform.position;
            float deltaX = _followTarget.position.x * _effectValue;

            if (_isInverse == true)
                deltaX *= -1;

            _defaultPosition += Vector3.right * (_defaultEffectValue * (_isInverse ? -1 : 1) * Time.deltaTime);
            transform.position = new Vector3(_startPositionX + deltaX, position.y, position.z) + _defaultPosition;
        }
    }
}