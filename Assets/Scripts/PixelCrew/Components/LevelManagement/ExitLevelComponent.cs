using PixelCrew.Model;
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
            
            if (session != null)
                session.SaveData();

            SceneManager.LoadScene(_name);
        }
    }
}