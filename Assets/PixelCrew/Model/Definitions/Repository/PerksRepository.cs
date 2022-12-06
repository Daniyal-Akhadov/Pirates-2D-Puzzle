using System;
using PixelCrew.Model.Definitions.Repository.Items;
using PixelCrew.Utilities.TimeManagement;
using UnityEngine;

namespace PixelCrew.Model.Definitions.Repository
{
    [CreateAssetMenu(menuName = "Definitions/PerkRepository", fileName = "PerkRepository")]
    public class PerksRepository : DefinitionsRepository<PerkDefinition>
    {
    }

    [Serializable]
    public class PerkDefinition : IHaveId
    {
        [SerializeField] private string _id;
        [SerializeField] private Sprite _icon;
        [SerializeField] private string _information;
        [SerializeField] private ItemWithCount _price;
        [SerializeField] private Cooldown _cooldown;

        public string Id => _id;

        public Sprite Icon => _icon;

        public string Information => _information;

        public ItemWithCount Price => _price;

        public Cooldown Cooldown => _cooldown;
    }

    [Serializable]
    public struct ItemWithCount
    {
        [InventoryId] [SerializeField] private string itemId;
        [SerializeField] private int _count;

        public string ItemId => itemId;

        public int Count => _count;
    }
}