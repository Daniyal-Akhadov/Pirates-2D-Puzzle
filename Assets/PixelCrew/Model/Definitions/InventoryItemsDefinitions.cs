using System;
using System.Linq;
using UnityEngine;

namespace PixelCrew.Model.Definitions
{
    [CreateAssetMenu(menuName = "Definitions/InventoryItems", fileName = "InventoryItemDefinitions")]
    public class InventoryItemsDefinitions : ScriptableObject
    {
        [SerializeField] private ItemDefinition[] _items;

        public ItemDefinition Get(string id)
        {
            ItemDefinition result = default;

            foreach (var item in _items)
            {
                if (item.Id == id)
                {
                    result = item;
                    break;
                }
            }

            return result;
        }

#if UNITY_EDITOR
        public ItemDefinition[] DefinitionsForEditor => _items;
#endif
    }

    [Serializable]
    public struct ItemDefinition
    { 
        [SerializeField] private string _id;
        [SerializeField] private Sprite _icon;
        [SerializeField] private ItemTag[] _tags;
        
        public string Id => _id;

        public bool IsVoid => string.IsNullOrEmpty(_id);

        public Sprite Icon => _icon;

        public bool HasTag(ItemTag tag)
        {
            return _tags.Contains(tag);
        }
    }
}