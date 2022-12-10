using PixelCrew.Model;
using PixelCrew.UI.LevelsLoader;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PixelCrew.Components.LevelManagement
{
    public class ReloadSceneComponent : MonoBehaviour
    {
        private bool _isStart;

        public void Reload()
        {
            if (_isStart == true)
                return;
            
            _isStart = true;
            
            var session = FindObjectOfType<GameSession>();
            session.LoadLastSave();

            var levelLoader = FindObjectOfType<LevelLoader>();
            var currentScene = SceneManager.GetActiveScene();
            levelLoader.LoadScene(currentScene.name);
        }
    }
}