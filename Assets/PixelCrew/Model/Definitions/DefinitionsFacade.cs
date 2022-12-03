using PixelCrew.Model.Definitions.Repository;
using PixelCrew.Model.Definitions.Repository.Items;
using UnityEngine;

namespace PixelCrew.Model.Definitions
{
    [CreateAssetMenu(menuName = "Definitions/DefinitionsFacade", fileName = "DefinitionsFacade")]
    public class DefinitionsFacade : ScriptableObject
    {
        [SerializeField] private ItemsRepository _items;
        [SerializeField] private ThrowableRepository _throwableItems;
        [SerializeField] private PotionRepository _potionRepository;
        [SerializeField] private PlayerDefinitions _playerDefinitions;

        private static DefinitionsFacade _instance;

        public ItemsRepository Items => _items;
        public PlayerDefinitions PlayerDefinitions => _playerDefinitions;
        public ThrowableRepository ThrowableRepository => _throwableItems;

        public PotionRepository PotionRepository => _potionRepository;

        public static DefinitionsFacade Instance => _instance == null ? LoadDefinitions() : _instance;

        private static DefinitionsFacade LoadDefinitions()
        {
            _instance = Resources.Load<DefinitionsFacade>("DefinitionsFacade");
            return _instance;
        }
    }
}