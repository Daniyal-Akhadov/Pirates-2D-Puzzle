using System.Collections.Generic;
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

        private readonly List<InventoryItemWidget> _createdItems = new();
        private readonly CompositeDisposable _trash = new();

        private void Start()
        {
            _session = FindObjectOfType<GameSession>();
            _trash.Retain(_session.QuickInventory.Subscribe(Rebuild));
            Rebuild();
        }

        private void OnDestroy()
        {
            _trash?.Dispose();
        }

        private void Rebuild()
        {
            var quickInventory = _session.QuickInventory.Inventory;

            for (int i = _createdItems.Count; i < quickInventory.Length; i++)
            {
                var item = Instantiate(_item, _container);
                _createdItems.Add(item);
            }

            for (int i = 0; i < quickInventory.Length; i++)
            {
                _createdItems[i].SetData(quickInventory[i], i);
                _createdItems[i].gameObject.SetActive(true);
            }

            for (int i = quickInventory.Length; i < _createdItems.Count; i++)
            {
                _createdItems[i].gameObject.SetActive(false);
            }
        }
    }
}