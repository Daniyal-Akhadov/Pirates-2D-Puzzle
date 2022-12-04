using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PixelCrew.UI.MainMenu
{
    public class PauseMenuWindow : AnimatedWindow
    {
        private bool _isPanelActive;
        private Action _closeAction;

        protected override void Awake()
        {
            base.Awake();
            gameObject.SetActive(false);
        }

        public void OnPauseButtonClick()
        {
            _isPanelActive = !_isPanelActive;


            if (_isPanelActive == true)
            {
                gameObject.SetActive(true);
                Show();
            }
            else
                Close();
        }

        public void OnShowSettings()
        {
            var window = Resources.Load<GameObject>("UI/SettingsWindow");
            var canvas = FindObjectOfType<Canvas>();
            Instantiate(window, canvas.transform);
        }

        public void OnRestart()
        {
            _closeAction = () => { SceneManager.LoadScene(SceneManager.GetActiveScene().name); };
            Close();
        }

        public void OnExit()
        {
            _closeAction = () =>
            {
                SceneManager.LoadScene("MainMenu");

#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#endif
            };

            Close();
        }

        public override void OnCloseAnimationComplete()
        {
            _closeAction?.Invoke();
        }
    }
}