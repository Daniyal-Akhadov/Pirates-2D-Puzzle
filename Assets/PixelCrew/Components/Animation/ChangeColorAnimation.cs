using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace PixelCrew.Components.Animation
{
    public class ChangeColorAnimation : MonoBehaviour
    {
        [SerializeField] private float _duration = 0.5f;
        [ColorUsage(true, true)] [SerializeField] private Color _color = new(205, 205, 205);
        [SerializeField] private SpriteRenderer[] _spriteRenderers;
        [SerializeField] private Color _originalColor = new(140f, 140f, 140f);

        private readonly List<Tween> _tweens = new();

        public void StartChangeColor()
        {
            StartCoroutine(nameof(ChangeColor));
        }

        private IEnumerator ChangeColor()
        {
            foreach (var spriteRenderer in _spriteRenderers)
            {
                spriteRenderer.color = _color;

                _tweens.Add(spriteRenderer.DOColor(_originalColor, _duration));
                yield return null;
            }
        }

        private void OnDestroy()
        {
            foreach (var tween in _tweens)
            {
                tween?.Kill();
            }
        }
    }
}