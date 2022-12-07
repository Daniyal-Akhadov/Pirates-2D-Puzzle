using System.Linq;
using UnityEngine;

namespace PixelCrew.Model.Definitions.Player
{
    [CreateAssetMenu(menuName = "Definitions/PlayerDefinitions", fileName = "PlayerDefinitions")]
    public class PlayerDefinitions : ScriptableObject
    {
        [SerializeField] private int _inventorySize;
        [SerializeField] private StatDef[] _stats;
        
        public int InventorySize => _inventorySize;

        public StatDef[] Stats => _stats;


        public StatDef GetStat(StatId id) => _stats.FirstOrDefault(stat => stat.Id == id);

    }
}