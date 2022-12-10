using System;
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

        public FloatProperty Fuel = new();

        public PerksData PerksData = new();

        public PlayerData Clone()
        {
            string json = JsonUtility.ToJson(this);
            return JsonUtility.FromJson<PlayerData>(json);
        }
    }
}