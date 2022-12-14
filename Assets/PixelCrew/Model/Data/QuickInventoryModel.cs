using System;
using System.Collections.Generic;
using System.Linq;
using PixelCrew.Model.Data.Properties;
using PixelCrew.Model.Definitions;
using PixelCrew.Model.Definitions.Repository.Items;
using PixelCrew.Utilities.Disposables;
using UnityEngine;

namespace PixelCrew.Model.Data
{
    public class QuickInventoryModel : IDisposable
    {
        private readonly PlayerData _playerData;

        public readonly IntProperty SelectedIndex = new();
        private readonly GameSession _session;
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

        public ItemDefinition SelectedItemDefinition => DefinitionsFacade.Instance.Items.Get(SelectedItem?.Id);

        private event Action OnQuickInventoryChanged;

        public QuickInventoryModel(PlayerData data, GameSession session)
        {
            _playerData = data;
            _session = session;
            Inventory = _playerData.Inventory.GetAll(ItemTag.Usable).Take(3).ToList();
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
            SelectedIndex.Value = (int)Mathf.Repeat(1 + SelectedIndex.Value, Inventory.Count);
        }

        public bool Has(InventoryItemData item)
        {
            return Inventory.Contains(item);
        }

        private void OnInventoryChanged(string id, int value)
        {
            var quickSize = DefinitionsFacade.Instance.PlayerDefinitions.InventorySize;
            Inventory = _session.QuickInventory.Inventory.Take(quickSize).ToList();
            SelectedIndex.Value = Mathf.Clamp(SelectedIndex.Value, 0, Inventory.Count - 1);
            OnQuickInventoryChanged?.Invoke();
        }
    }
}