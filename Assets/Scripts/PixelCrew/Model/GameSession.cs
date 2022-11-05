using System.Linq;
using UnityEngine;

namespace PixelCrew.Model
{
    public class GameSession : MonoBehaviour
    {
        [SerializeField] private PlayerData _data;

        private PlayerData _saveData;

        public PlayerData Data => _data;

        private void Awake()
        {
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