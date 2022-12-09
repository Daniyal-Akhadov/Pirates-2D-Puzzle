using PixelCrew.Model.Data;
using PixelCrew.UI.Widgets;
using UnityEngine;

namespace PixelCrew.UI.Windows.Settings
{
    public class SettingsWindow : AnimatedWindow
    {
        [SerializeField] private AudioSettingsWidget _music;
        [SerializeField] private AudioSettingsWidget _sfx;

        protected override void Awake()
        {
            base.Awake();
            _music.SetModel(GameSettingsData.Instance.Music);
            _sfx.SetModel(GameSettingsData.Instance.Sfx);
        }
    }
}