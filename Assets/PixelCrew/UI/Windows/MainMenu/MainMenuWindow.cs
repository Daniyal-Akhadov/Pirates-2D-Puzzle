using System;
using PixelCrew.UI.LevelsLoader;
using PixelCrew.Utilities;
using UnityEngine;

namespace PixelCrew.UI.Windows.MainMenu
{
    public class MainMenuWindow : AnimatedWindow
    {
        private Action _closeAction;

        public void OnShowLocalizationWindow()
        {
            WindowUtils.CreateWindow("UI/LocalizationWindow");
        }

        public void OnShowSettings()
        {
            WindowUtils.CreateWindow("UI/SettingsWindow");
        }

        public void OnStartGame()
        {
            var levelLoader = FindObjectOfType<LevelLoader>();
            _closeAction = () => { levelLoader.LoadScene("Level1"); };
            Close();
        }

        public void OnExit()
        {
            _closeAction = () =>
            {
                Application.Quit();

#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#endif
            };

            Close();
        }

        public override void OnCloseAnimationComplete()
        {
            base.OnCloseAnimationComplete();
            _closeAction?.Invoke();
        }
    }
}