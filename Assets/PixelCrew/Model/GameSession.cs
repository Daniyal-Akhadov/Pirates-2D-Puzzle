using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PixelCrew.Model
{
    public class GameSession : MonoBehaviour
    {
        [SerializeField] private PlayerData _data;

        private PlayerData _saveData;

        public PlayerData Data => _data;

        private void Awake()
        {
            LoadHud();
            
            if (IsSessionExit())
            {
                Destroy(gameObject);
            }
            else
            {
                SaveData();
                DontDestroyOnLoad(this);
            }
        }

        private void LoadHud()
        {
            SceneManager.LoadScene("Hud", LoadSceneMode.Additive);
        }

        public void SaveData()
        {
            _saveData = _data.Clone();
        }

        public void LoadLastSave()
        {
            _data = _saveData.Clone();
        }

        private bool IsSessionExit()
        {
            var sessions = FindObjectsOfType<GameSession>();
            return sessions.Any(session => session != this);
        }
    }
}