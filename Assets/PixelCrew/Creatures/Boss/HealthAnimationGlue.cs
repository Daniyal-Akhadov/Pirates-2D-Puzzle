using PixelCrew.Creatures.Core.Health;
using PixelCrew.Utilities.Disposables;
using UnityEngine;

namespace PixelCrew.Creatures.Boss
{
    public class HealthAnimationGlue : MonoBehaviour
    {
        [SerializeField] private HealthComponent _health;
        [SerializeField] private Animator _animator;

        private static readonly int Health = Animator.StringToHash("health");

        private readonly CompositeDisposable _trash = new();

        private void Awake()
        {
            _trash.Retain(_health._onChanged.Subscribe(OnHealthChanged));
            OnHealthChanged(_health.Health);
        }

        private void OnDestroy()
        {
            _trash?.Dispose();
        }

        private void OnHealthChanged(int value)
        {
            _animator.SetFloat(Health, value);
        }
    }
}