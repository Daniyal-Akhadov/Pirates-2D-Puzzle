using PixelCrew.Model;
using PixelCrew.UI.LevelsLoader;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PixelCrew.Components.LevelManagement
{
    public class ExitLevelComponent : MonoBehaviour
    {
        [SerializeField] private string _name;

        public void Exit()
        {
            var session = FindObjectOfType<GameSession>();
            session.SaveData();

            var levelLoader = FindObjectOfType<LevelLoader>();
            levelLoader.LoadScene(_name);
        }
    }
}