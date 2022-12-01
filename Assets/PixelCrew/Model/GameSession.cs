using System;
using System.Linq;
using PixelCrew.Model.Data;
using PixelCrew.Utilities.Disposables;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PixelCrew.Model
{
    public class GameSession : MonoBehaviour
    {
        [SerializeField] private PlayerData _data;

        private PlayerData _saveData;
        private readonly CompositeDisposable _trash = new();

        public PlayerData Data => _data;
        public QuickInventoryModel QuickInventory { get; private set; }

        private void Awake()
        {
            LoadHud();

            if (IsSessionExit())
            {
                Destroy(gameObject);
            }
            else
            {
                InitModels();
                SaveData();
                DontDestroyOnLoad(this);
            }
        }

        private void OnDestroy()
        {
            _trash?.Dispose();
        }

        public void SaveData()
        {
            _saveData = _data.Clone();
        }

        public void LoadLastSave()
        {
            _data = _saveData.Clone();
            _trash?.Dispose();
            InitModels();
        }

        private void InitModels()
        {
            QuickInventory = new QuickInventoryModel(Data);
            _trash.Retain(QuickInventory);
        }

        private void LoadHud()
        {
            SceneManager.LoadScene("Hud", LoadSceneMode.Additive);
        }

        private bool IsSessionExit()
        {
            var sessions = FindObjectsOfType<GameSession>();
            return sessions.Any(session => session != this);
        }
    }
}