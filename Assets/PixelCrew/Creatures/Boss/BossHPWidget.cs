using PixelCrew.Creatures.Core.Health;
using PixelCrew.UI.Widgets;
using PixelCrew.Utilities;
using PixelCrew.Utilities.Disposables;
using UnityEngine;

namespace PixelCrew.Creatures.Boss
{
    [RequireComponent(typeof(CanvasGroup))]
    public class BossHPWidget : MonoBehaviour
    {
        [SerializeField] private HealthComponent _health;
        [SerializeField] private ProgressBarWidget _hpBar;
        [SerializeField] private CanvasGroup _group;

        private float _maxHealth;
        private readonly CompositeDisposable _trash = new();

        private void Start()
        {
            _maxHealth = _health.Health;
            _trash.Retain(_health._onChanged.Subscribe(OnHpChanged));
            _trash.Retain(_health._onDie.Subscribe(HideUI));
        }

        public void ShowUI()
        {
            this.LerpAnimated(0, 1, 1, SetAlpha);
        }

        private void HideUI()
        {
            this.LerpAnimated(1, 0, 1, SetAlpha);
        }

        private void SetAlpha(float alpha)
        {
            _group.alpha = alpha;
        }

        private void OnDestroy()
        {
            _trash?.Dispose();
        }

        private void OnHpChanged(int value)
        {
            _hpBar.SetProgress(value / _maxHealth);
        }
    }
}