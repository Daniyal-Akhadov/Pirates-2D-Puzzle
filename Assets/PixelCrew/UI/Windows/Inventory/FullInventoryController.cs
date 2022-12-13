using System.Linq;
using PixelCrew.Model;
using PixelCrew.Model.Data;
using PixelCrew.Model.Definitions;
using PixelCrew.UI.Hud.QuickInventory;
using PixelCrew.UI.Widgets;
using PixelCrew.Utilities.Disposables;
using UnityEngine;
using UnityEngine.UI;

namespace PixelCrew.UI.Windows.Inventory
{
    public class FullInventoryController : MonoBehaviour
    {
        [SerializeField] private Transform _container;
        [SerializeField] private Button _putButton;

        private GameSession _session;
        private QuickInventoryController _quickInventoryController;

        private readonly CompositeDisposable _trash = new();
        private PredefinedDataGroup<InventoryItemData, InventoryItemWidget> _dataGroup;
        
        private CanvasGroup _canvasGroup;
        public CanvasGroup CanvasGroup => _canvasGroup;

        private void Start()
        {
            _quickInventoryController = FindObjectOfType<QuickInventoryController>();
            _session = FindObjectOfType<GameSession>();
            _canvasGroup = GetComponentInChildren<CanvasGroup>();
            _dataGroup = new PredefinedDataGroup<InventoryItemData, InventoryItemWidget>(_container);
            _trash.Retain(_session.FullInventoryModel.Subscribe(Rebuild));
            _trash.Retain(_session.FullInventoryModel.Subscribe(OnInventoryChanged));
            _trash.Retain(_putButton.onClick.Subscribe(OnPutButton));
            Rebuild(default);
        }

        private void OnPutButton()
        {
            var selectedItem = _session.FullInventoryModel.SelectedItem;

            if (_session.FullInventoryModel.Inventory.Count <= 0 || _session.QuickInventory.Has(selectedItem))
                return;

            var quickInventory = _session.QuickInventory.Inventory;

            if (quickInventory.Count >= DefinitionsFacade.Instance.PlayerDefinitions.InventorySize)
            {
                var removedItem = quickInventory.Last();
                removedItem.InQuick = false;
                quickInventory.Remove(removedItem);
                quickInventory.Insert(0, selectedItem);
            }
            else
            {
                quickInventory.Add(selectedItem);
            }

            selectedItem.InQuick = true;
            _quickInventoryController.Rebuild();
        }

        private void OnInventoryChanged(InventoryItemData item)
        {
            if (item == null)
                return;

            var quickInventory = _session.QuickInventory.Inventory;
            var inventorySize = DefinitionsFacade.Instance.PlayerDefinitions.InventorySize;

            if (_session.FullInventoryModel.Inventory.Count <= 0 && quickInventory.Count >= inventorySize)
                return;

            if (quickInventory.Any(inventoryItemData => inventoryItemData.Id == item.Id))
            {
                return;
            }

            item.InQuick = true;
            quickInventory.Add(item);
            quickInventory.ForEach(inventoryItemData => print(inventoryItemData.ToString()));
            _quickInventoryController.Rebuild();
        }

        private void Rebuild(InventoryItemData item)
        {
            _dataGroup.SetData(_session.Data.Inventory.GetAll(), false);
        }

        private void OnDestroy()
        {
            _trash?.Dispose();
        }
    }
}