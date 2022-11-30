using System;
using PixelCrew.Model;
using PixelCrew.Utilities.Disposables;
using UnityEngine;

namespace PixelCrew.UI.Hud.QuickInventory
{
    public class QuickInventoryController : MonoBehaviour
    {
        [SerializeField] private Transform _container;
        [SerializeField] private InventoryItemWidget _item;

        private GameSession _session;
        private InventoryItemData[] _inventory;

        private readonly CompositeDisposable _trash = new CompositeDisposable();

        private void Start()
        {
            _session = FindObjectOfType<GameSession>();
            _inventory = _session.Data.Inventory.GetAll();
            
            Rebuild();
        }

        private void Rebuild()
        {
            // _inventory
        }
    }
}