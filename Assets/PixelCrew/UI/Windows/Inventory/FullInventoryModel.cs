using System;
using System.Collections.Generic;
using System.Linq;
using PixelCrew.Model.Data;
using PixelCrew.Model.Data.Properties;
using PixelCrew.Utilities.Disposables;
using UnityEngine;

namespace PixelCrew.UI.Windows.Inventory
{
    public class FullInventoryModel : IDisposable
    {
        private readonly PlayerData _playerData;
        public readonly IntProperty SelectedIndex = new();
        public List<InventoryItemData> Inventory { get; private set; }

        public InventoryItemData SelectedItem
        {
            get
            {
                InventoryItemData result = null;

                if (Inventory.Count > 0 && Inventory.Count > SelectedIndex.Value)
                {
                    result = Inventory[SelectedIndex.Value];
                }

                return result;
            }
        }

        private event Action<InventoryItemData> OnFullInventoryChanged;

        public FullInventoryModel(PlayerData data)
        {
            _playerData = data;
            Inventory = _playerData.Inventory.GetAll().ToList();
            _playerData.Inventory.OnChanged += OnInventoryChanged;
        }

        public IDisposable Subscribe(Action<InventoryItemData> call)
        {
            OnFullInventoryChanged += call;
            return new ActionDisposable(() => OnFullInventoryChanged -= call);
        }

        public void SetNextItem()
        {
            SelectedIndex.Value = (int)Mathf.Repeat(1 + SelectedIndex.Value, Inventory.Count);
        }

        public void Dispose()
        {
            _playerData.Inventory.OnChanged -= OnInventoryChanged;
        }

        private void OnInventoryChanged(string id, int value)
        {
            Inventory = _playerData.Inventory.GetAll().ToList();
            SelectedIndex.Value = Mathf.Clamp(SelectedIndex.Value, 0, Inventory.Count - 1);
            var item = _playerData.Inventory.GetItem(id);
            OnFullInventoryChanged?.Invoke(item);
        }
    }
}