using UnityEngine;
using UnityEngine.Events;

namespace PixelCrew.Utilities
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class SpriteAnimation : MonoBehaviour
    {
        [SerializeField] private int _frameRate = 10;
        [SerializeField] private bool _isLoop;
        [SerializeField] private Sprite[] _sprites;
        [SerializeField] private UnityEvent _onComplete;

        private SpriteRenderer _renderer;
        private float _secondsPerFrame;
        private float _timeToUpdateSprite;
        private int _currentSpriteIndex;
        private bool _isStopped;

        private const float OneSecond = 1f;

        private void Awake()
        {
            _renderer = GetComponent<SpriteRenderer>();
        }

        private void OnEnable()
        {
            _secondsPerFrame = OneSecond / _frameRate;
            _timeToUpdateSprite = Time.time;
            _currentSpriteIndex = 0;
        }

        private void Update()
        {
            if (_timeToUpdateSprite > Time.time)
                return;

            _renderer.sprite = _sprites[_currentSpriteIndex++];
            _timeToUpdateSprite += _secondsPerFrame;

            if (_currentSpriteIndex >= _sprites.Length)
            {
                if (_isLoop == true)
                {
                    _currentSpriteIndex = 0;
                }
                else
                {
                    _onComplete?.Invoke();
                    enabled = false;
                }
            }
        }
    }
}