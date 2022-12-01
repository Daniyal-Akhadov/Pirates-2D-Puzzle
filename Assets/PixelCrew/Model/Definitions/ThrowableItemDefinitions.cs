using System;
using UnityEngine;

namespace PixelCrew.Model.Definitions
{
    [CreateAssetMenu(menuName = "Definitions/ThrowableItemDefinitions", fileName = "ThrowableItemDefinitions")]
    public class ThrowableItemDefinitions : ScriptableObject
    {
        [SerializeField] private ThrowableItemDefinition[] _items;

        public ThrowableItemDefinition Get(string id)
        {
            foreach (var item in _items)
            {
                if (item.Id == id)
                    return item;
            }

            return default;
        }
    }

    [Serializable]
    public struct ThrowableItemDefinition
    {
        [InventoryId] [SerializeField] private string _id;
        [SerializeField] private GameObject _projectile;

        public string Id => _id;
        
        public GameObject Projectile => _projectile;
    }
}