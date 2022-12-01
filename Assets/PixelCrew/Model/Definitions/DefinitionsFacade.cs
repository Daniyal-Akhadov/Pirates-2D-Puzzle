using UnityEngine;

namespace PixelCrew.Model.Definitions
{
    [CreateAssetMenu(menuName = "Definitions/DefinitionsFacade", fileName = "DefinitionsFacade")]
    public class DefinitionsFacade : ScriptableObject
    {
        [SerializeField] private InventoryItemsDefinitions _items;
        [SerializeField] private PlayerDefinitions _playerDefinitions;
        [SerializeField] private ThrowableItemDefinitions _throwableItems;

        private static DefinitionsFacade _instance;

        public InventoryItemsDefinitions Items => _items;
        public PlayerDefinitions PlayerDefinitions => _playerDefinitions;
        public ThrowableItemDefinitions ThrowableItemDefinitions => _throwableItems;

        public static DefinitionsFacade Instance => _instance == null ? LoadDefinitions() : _instance;

        private static DefinitionsFacade LoadDefinitions()
        {
            _instance = Resources.Load<DefinitionsFacade>("DefinitionsFacade");
            return _instance;
        }
    } 
}