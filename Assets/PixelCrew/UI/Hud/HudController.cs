using PixelCrew.Model;
using PixelCrew.Model.Definitions.Player;
using PixelCrew.UI.Widgets;
using PixelCrew.Utilities;
using UnityEngine;

namespace PixelCrew.UI.Hud
{
    public class HudController : MonoBehaviour
    {
        [SerializeField] private ProgressBarWidget _healthBar;

        private GameSession _session;

        private const string Tag = "Hud";

        private void Start()
        {
            _session = FindObjectOfType<GameSession>();
            _session.Data.Health.OnChanged += OnHealthChanged;
            OnHealthChanged(_session.Data.Health.Value, 0);
        }

        private void OnDestroy()
        {
            _session.Data.Health.OnChanged -= OnHealthChanged;
        }

        public void OnInGameSettings()
        {
            WindowUtils.CreateWindow("UI/InGameMenuWindow", Tag);
        }

        public void OnDebug()
        {
            WindowUtils.CreateWindow("UI/FullInventory", Tag);
        }
        
        private void OnHealthChanged(int newValue, int _)
        {
            var maxHealth = _session.StatsModel.GetValue(StatId.Hp);
            var value = (float)newValue / maxHealth;
            _healthBar.SetProgress(value);
        }
    }
}