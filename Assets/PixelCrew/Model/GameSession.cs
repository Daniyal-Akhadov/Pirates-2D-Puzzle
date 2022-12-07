using System;
using System.Collections.Generic;
using System.Linq;
using PixelCrew.Components.LevelManagement;
using PixelCrew.Model.Data;
using PixelCrew.Model.Definitions.Player;
using PixelCrew.Model.Models;
using PixelCrew.Utilities.Disposables;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PixelCrew.Model
{
    public class GameSession : MonoBehaviour
    {
        [SerializeField] private PlayerData _data;
        [SerializeField] private string _defaultCheckPoint;

        private PlayerData _saveData;
        private readonly CompositeDisposable _trash = new();
        private readonly List<string> _checkPointsChecked = new();

        public PlayerData Data => _data;
        public QuickInventoryModel QuickInventory { get; private set; }
        public PerksModel PerksModel { get; private set; }
        
        public StatsModel StatsModel { get; private set; }

        public bool IsDefaultCheckPoint
        {
            get
            {
                bool result = false;

                if (HasCheckpoints == true)
                    result = _checkPointsChecked.Last() == _defaultCheckPoint;

                return result;
            }
        }

        private bool HasCheckpoints => _checkPointsChecked.Count != 0;


        private void Awake()
        {
            var existSessions = GetExistsSession();

            if (existSessions != null)
            {
                existSessions.StartSession(_defaultCheckPoint);
                Destroy(gameObject);
            }
            else
            {
                InitModels();
                SaveData();
                DontDestroyOnLoad(this);
                StartSession(_defaultCheckPoint);
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

        public bool IsChecked(string id)
        {
            return _checkPointsChecked.Contains(id);
        }

        public void SetChecked(string id)
        {
            if (_checkPointsChecked.Contains(id) == false)
            {
                SaveData();
                _checkPointsChecked.Add(id);
            }
        }

        private void SpawnHero()
        {
            var checkPoints = FindObjectsOfType<CheckPointComponent>();
            string lastPointId = _checkPointsChecked.Last();

            foreach (var point in checkPoints)
            {
                if (point.Id == lastPointId)
                {
                    point.SpawnHero();
                    break;
                }
            }
        }

        private void InitModels()
        {
            QuickInventory = new QuickInventoryModel(Data);
            _trash.Retain(QuickInventory);

            PerksModel = new PerksModel(Data);
            _trash.Retain(PerksModel);

            StatsModel = new StatsModel(Data);
            _trash.Retain(StatsModel);

            _data.Health.Value = (int)StatsModel.GetValue(StatId.Hp);
        }

        private void LoadHud()
        {
            SceneManager.LoadScene("Hud", LoadSceneMode.Additive);
        }

        private void StartSession(string defaultCheckPoint)
        {
            SetChecked(defaultCheckPoint);
            LoadHud();
            SpawnHero();
        }

        private GameSession GetExistsSession()
        {
            var sessions = FindObjectsOfType<GameSession>();
            return sessions.FirstOrDefault(session => session != this);
        }

        private readonly List<string> _removedItems = new();

        public void StoreState(string id)
        {
            if (_removedItems.Contains(id) == false)
                _removedItems.Add(id);
        }

        public bool RestoreState(string id)
        {
            return _removedItems.Contains(id);
        }
    }
}