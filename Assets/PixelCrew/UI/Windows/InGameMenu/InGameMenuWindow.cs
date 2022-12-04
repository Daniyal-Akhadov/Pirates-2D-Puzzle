using PixelCrew.Model;
using PixelCrew.Utilities;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PixelCrew.UI.InGameMenu
{
    public class InGameMenuWindow : AnimatedWindow
    {
        private float _defaultTimeScale;

        protected override void Start()
        {
            base.Start();

            _defaultTimeScale = Time.timeScale;
            Time.timeScale = 0f;
        }

        private void OnDestroy()
        {
            Time.timeScale = _defaultTimeScale;
        }

        public void OnShowSettings()
        {
            WindowUtils.CreateWindow("UI/SettingsWindow");
        }

        public void OnExit()
        {
            SceneManager.LoadScene("MainMenu");
            
            var session = FindObjectOfType<GameSession>();
            Destroy(session.gameObject);
        }
    }
}