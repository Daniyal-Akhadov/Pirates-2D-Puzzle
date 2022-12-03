﻿using PixelCrew.Model;
using PixelCrew.Model.Definitions;
using PixelCrew.UI.Widgets;
using PixelCrew.Utilities;
using UnityEngine;

namespace PixelCrew.UI.Hud
{
    public class HudController : MonoBehaviour
    {
        [SerializeField] private ProgressBarWidget _healthBar;
      
        private GameSession _session;

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
            WindowUtils.CreateWindow("UI/InGameMenuWindow");
        }
        
        private void OnHealthChanged(int newValue, int oldValue)
        {
            var maxHealth = DefinitionsFacade.Instance.PlayerDefinitions.MaxHealth;
            var value = (float)newValue / maxHealth;
            _healthBar.SetProgress(value);
        }
    }
}