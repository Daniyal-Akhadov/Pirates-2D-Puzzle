using System;
using PixelCrew.Model;
using PixelCrew.Model.Data;
using PixelCrew.Model.Definitions;
using PixelCrew.Utilities.Disposables;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PixelCrew.UI.Hud.QuickInventory
{
    public class InventoryItemWidget : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private GameObject _selection;
        [SerializeField] private TMP_Text _value;

        private readonly CompositeDisposable _trash = new();
        private int _index;

        private void Start()
        {
            var session = FindObjectOfType<GameSession>();
            _trash.Retain(session.QuickInventory.SelectedIndex.SubscribeAndInvoke(OnIndexChanged));
        }

        private void OnDestroy()
        {
            _trash?.Dispose();
        }

        private void OnIndexChanged(int newValue, int _)
        {
            _selection.SetActive(_index == newValue);
        }

        public void SetData(InventoryItemData item, int index)
        {
            _index = index;
            
            var definition = DefinitionsFacade.Instance.Items.Get(item.Id);
            _icon.sprite = definition.Icon;
            _value.text = definition.HasTag(ItemTag.Stackable) == true ? 
                item.Value.ToString() : string.Empty;
        }
    }
}