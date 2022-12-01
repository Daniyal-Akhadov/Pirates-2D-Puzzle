using System;
using PixelCrew.Model.Data.Properties;
using PixelCrew.Model.Definitions;
using PixelCrew.Utilities.Disposables;
using UnityEngine;

namespace PixelCrew.Model.Data
{
    public class QuickInventoryModel : IDisposable
    {
        private readonly PlayerData _playerData;

        public readonly IntProperty SelectedIndex = new();
        public InventoryItemData[] Inventory { get; private set; }
        public InventoryItemData SelectedItem => Inventory[SelectedIndex.Value];
        
        private event Action OnQuickInventoryChanged;

        public QuickInventoryModel(PlayerData data)
        {
            _playerData = data;
            Inventory = _playerData.Inventory.GetAll(ItemTag.Usable);
            _playerData.Inventory.OnChanged += OnInventoryChanged;
        }

        public void Dispose()
        {
            _playerData.Inventory.OnChanged -= OnInventoryChanged;
        }

        public IDisposable Subscribe(Action call)
        {
            OnQuickInventoryChanged += call;
            return new ActionDisposable(() => OnQuickInventoryChanged -= call);
        }

        public void SetNextItem()
        {
            SelectedIndex.Value = (int)Mathf.Repeat(1 + SelectedIndex.Value, Inventory.Length);
        }

        private void OnInventoryChanged(string id, int value)
        {
            Inventory = _playerData.Inventory.GetAll(ItemTag.Usable);
            SelectedIndex.Value = Mathf.Clamp(SelectedIndex.Value, 0, Inventory.Length - 1);
            OnQuickInventoryChanged?.Invoke();
        }
    }
}