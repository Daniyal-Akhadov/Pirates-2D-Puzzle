using System;
using PixelCrew.Model.Definitions.Repository.Items;
using UnityEngine;

namespace PixelCrew.Model.Definitions.Repository
{
    [CreateAssetMenu(menuName = "Definitions/ThrowableRepository", fileName = "ThrowableRepository")]
    public class ThrowableRepository : DefinitionsRepository<ThrowableItemDefinition>
    {

    }

    [Serializable]
    public struct ThrowableItemDefinition : IHaveId
    {
        [InventoryId] [SerializeField] private string _id;
        [SerializeField] private GameObject _projectile;

        public string Id => _id;
        
        public GameObject Projectile => _projectile;
    }
}