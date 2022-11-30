using System.Collections;
using UnityEngine;

namespace PixelCrew.Components
{
    public class DynamicPlatform : MonoBehaviour
    {
        [SerializeField] private Vector2 _targetScale;
        [SerializeField] private float _speed;

        private Vector2 _originalScale;
        private Vector2 _currentTarget;
        private bool _hasTargetScale;

        private void Awake()
        {
            _originalScale = transform.localScale;
        }

        private void Start()
        {
            StartCoroutine(Change());
        }

        private IEnumerator Change()
        {
            _currentTarget = _targetScale;

            while (enabled == true)
            {
                transform.localScale = Vector2.MoveTowards(
                    transform.localScale,
                    _currentTarget,
                    _speed * Time.deltaTime);

                if ((Vector2)transform.localScale == _targetScale)
                {
                    _currentTarget = _originalScale;
                }
                else if ((Vector2)transform.localScale == _originalScale)
                {
                    _currentTarget = _targetScale;
                }

                yield return null;
            }
        }
    }
}