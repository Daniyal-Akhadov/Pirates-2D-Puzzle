using System;
using PixelCrew.Model.Definitions.Repository.Items;
using UnityEngine;

namespace PixelCrew.Model.Definitions.Repository
{
    [CreateAssetMenu(menuName = "Definitions/PotionRepository", fileName = "PotionRepository")]
    public class PotionRepository : DefinitionsRepository<PotionDefinition>
    {
    }

    [Serializable]
    public struct PotionDefinition : IHaveId
    {
        [InventoryId] [SerializeField] private string _id;
        [SerializeField] private PotionEffect _effect;
        [SerializeField] private int _value;
        [SerializeField] private float _time;

        public PotionEffect Effect => _effect;
        public string Id => _id;
        public int Value => _value;

        public float Time => _time;
    }

    public enum PotionEffect
    {
        AddHp,
        SpeedUp
    }
}