using UnityEngine;

namespace PixelCrew.Model.Definitions
{
    [CreateAssetMenu(menuName = "Definitions/DefinitionsFacade", fileName = "DefinitionsFacade")]
    public class DefinitionsFacade : ScriptableObject
    {
        [SerializeField] private InventoryItemsDefinitions _items;
        [SerializeField] private PlayerDefinitions _playerDefinitions;
        public InventoryItemsDefinitions Items => _items;
        public PlayerDefinitions PlayerDefinitions => _playerDefinitions;

        private static DefinitionsFacade _instance;

        public static DefinitionsFacade Instance => _instance == null ? LoadDefinitions() : _instance;

        private static DefinitionsFacade LoadDefinitions()
        {
            _instance = Resources.Load<DefinitionsFacade>("DefinitionsFacade");
            return _instance;
        }
    }
}