using System;
using UnityEngine;
using UnityEngine.Events;

namespace PixelCrew.Utilities
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class SpriteAnimation : MonoBehaviour
    {
        [SerializeField] [Range(1, 30)] private int _frameRate = 10;
        [SerializeField] private AnimationClip[] _clips;
        [SerializeField] private UnityEvent<string> _onClipComplete;

        private SpriteRenderer _renderer;
        private float _secondsPerFrame;
        private float _timeToUpdateSprite;
        private int _currentSpriteIndex;
        private int _currentClipIndex;
        private bool _isStopped;
        private bool _isPlaying = true;

        private const float OneSecond = 1f;

        private void Awake()
        {
            _renderer = GetComponent<SpriteRenderer>();
            _secondsPerFrame = OneSecond / _frameRate;
            StartAnimation();
        }

        private void OnEnable()
        {
            _timeToUpdateSprite = Time.time;
        }

        private void OnBecameVisible()
        {
            enabled = _isPlaying;
        }

        private void OnBecameInvisible()
        {
            enabled = false;
        }

        private void Update()
        {
            if (_timeToUpdateSprite > Time.time)
                return;

            AnimationClip clip = _clips[_currentClipIndex];
            _renderer.sprite = clip.Sprites[_currentSpriteIndex++];
            _timeToUpdateSprite += _secondsPerFrame;

            if (_currentSpriteIndex >= clip.Sprites.Length)
            {
                if (clip.IsLoop == true)
                {
                    _currentSpriteIndex = 0;
                }
                else
                {
                    enabled = _isPlaying = clip.AllowNextClip;
                    clip.OnCompleted?.Invoke();
                    _onClipComplete?.Invoke(clip.Name);

                    if (clip.AllowNextClip == true)
                    {
                        _currentSpriteIndex = 0;
                        _currentClipIndex++;
                        _currentClipIndex %= _clips.Length;
                    }
                }
            }
        }

        public void SetClip(string name)
        {
            for (int i = 0; i < _clips.Length; i++)
            {
                if (_clips[i].Name == name)
                {
                    _currentClipIndex = i;
                    StartAnimation();
                }
            }
        }

        private void StartAnimation()
        {
            _timeToUpdateSprite = Time.time;
            _currentSpriteIndex = 0;
            enabled = _isPlaying = true;
        }
    }

    [Serializable]
    public class AnimationClip
    {
        [SerializeField] private string _name;
        [SerializeField] private Sprite[] _sprites;
        [SerializeField] private bool _isLoop;
        [SerializeField] private bool _allowNextClip;
        [SerializeField] private UnityEvent _onCompleted;

        public string Name => _name;
        public Sprite[] Sprites => _sprites;
        public bool IsLoop => _isLoop;
        public bool AllowNextClip => _allowNextClip;
        public UnityEvent OnCompleted => _onCompleted;
    }
}