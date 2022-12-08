using PixelCrew.Model;
using PixelCrew.Model.Data;
using PixelCrew.UI.Widgets;
using PixelCrew.Utilities.Disposables;
using UnityEngine;

namespace PixelCrew.UI.Hud.QuickInventory
{
    public class QuickInventoryController : MonoBehaviour
    {
        [SerializeField] private Transform _container;
        [SerializeField] private InventoryItemWidget _item;

        private GameSession _session;

        private readonly CompositeDisposable _trash = new();
        private DataGroup<InventoryItemData, InventoryItemWidget> _dataGroup;

        private void Start()
        {
            _session = FindObjectOfType<GameSession>();
            _trash.Retain(_session.QuickInventory.Subscribe(Rebuild));
            _dataGroup = new DataGroup<InventoryItemData, InventoryItemWidget>(_item, _container);
            Rebuild();
        }

        private void OnDestroy()
        {
            _trash?.Dispose();
        }

        public void Rebuild()
        {
            var quickInventory = _session.QuickInventory.Inventory;
            _dataGroup.SetData(quickInventory);
        }
    }
}