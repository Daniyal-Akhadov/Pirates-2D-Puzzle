using System;
using System.Collections.Generic;
using PixelCrew.Model.Data.Properties;
using UnityEngine;

namespace PixelCrew.Model.Data
{
    [Serializable]
    public class PlayerData
    {
        [SerializeField] private InventoryData _inventory;

        public InventoryData Inventory => _inventory;
        
        public LevelData LevelData = new();

        public IntProperty Health = new();

        public PerksData PerksData = new();

        public PlayerData Clone()
        {
            string json = JsonUtility.ToJson(this);
            return JsonUtility.FromJson<PlayerData>(json);
        }
    }

    [Serializable]
    public class PerksData
    {
        [SerializeField] private StringProperty _used = new();
        [SerializeField] private List<string> _unlocked;

        public StringProperty Used => _used;

        public void AddPerk(string id)
        {
            if (IsUnlocked(id) == false)
            {
                _unlocked.Add(id);
            }
        }

        public bool IsUnlocked(string id)
        {
            return _unlocked.Contains(id);
        }
    }
}