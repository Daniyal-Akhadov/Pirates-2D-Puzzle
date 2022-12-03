using System;
using System.Collections.Generic;
using System.Linq;
using PixelCrew.Model.Definitions;
using PixelCrew.Model.Definitions.Repository.Items;
using UnityEngine;
using UnityEngine.Events;

namespace PixelCrew.Model.Data
{
    [Serializable]
    public class InventoryData
    {
        [SerializeField] private List<InventoryItemData> _inventory = new();

        public event Action<string, int> OnChanged;

        public void Add(string id, int value, UnityEvent callback = null)
        {
            if (value <= 0) return;

            var itemDefinition = DefinitionsFacade.Instance.Items.Get(id);
            if (itemDefinition.IsVoid == true) return;


            if (itemDefinition.HasTag(ItemTag.Stackable))
            {
                var item = GetItem(id);

                if (item == null)
                {
                    item = new InventoryItemData(id);
                    _inventory.Add(item);
                }

                item.Value += value;
            }
            else
            {
                for (int i = 0; i < value; i++)
                {
                    var isFull = _inventory.Count >= DefinitionsFacade.Instance.PlayerDefinitions.InventorySize;
                    if (isFull == true) return;

                    var item = new InventoryItemData(id) { Value = 1 };
                    _inventory.Add(item);
                }
            }

            OnChanged?.Invoke(id, Count(id));
            callback?.Invoke();
        }

        public void Remove(string id, int value)
        {
            var itemDefinition = DefinitionsFacade.Instance.Items.Get(id);

            if (itemDefinition.IsVoid == true)
            {
                throw new ArgumentException($"You don't have this id: {id}!");
            }

            if (itemDefinition.HasTag(ItemTag.Stackable))
            {
                var item = GetItem(id);
                if (item == null) return;

                item.Value -= value;

                if (item.Value <= 0)
                    _inventory.Remove(item);
            }
            else
            {
                for (int i = 0; i < value; i++)
                {
                    var item = GetItem(id);
                    if (item == null) return;
                    _inventory.Remove(item);
                }
            }

            OnChanged?.Invoke(id, Count(id));
        }

        public int Count(string id)
        {
            return _inventory.Where(item => item.Id == id).Sum(item => item.Value);
        }

        public InventoryItemData[] GetAll(params ItemTag[] tags)
        {
            var result = new List<InventoryItemData>();
            
            foreach (var item in _inventory)
            {
                var definition = DefinitionsFacade.Instance.Items.Get(item.Id);
                bool isAllRequirementsMet = tags.All(tag => definition.HasTag(tag));
                
                if (isAllRequirementsMet == true)
                    result.Add(item);
            }

            return result.ToArray();
        }

        private InventoryItemData GetItem(string id)
        {
            return _inventory.FirstOrDefault(itemData => itemData.Id == id);
        }
    }

    [Serializable]
    public class InventoryItemData
    {
        [InventoryId] public string Id;
        public int Value;

        public InventoryItemData(string id)
        {
            Id = id;
        }
    }
}