using PixelCrew.Model;
using PixelCrew.Model.Data;
using PixelCrew.Model.Definitions;
using PixelCrew.Model.Definitions.Repository.Items;
using PixelCrew.UI.Widgets;
using PixelCrew.Utilities.Disposables;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PixelCrew.UI.Hud.QuickInventory
{
    public class InventoryItemWidget : MonoBehaviour, IItemRenderer<InventoryItemData>
    {
        [SerializeField] protected Image _icon;
        [SerializeField] private GameObject _selection;
        [SerializeField] protected TMP_Text _value;

        protected int Index = -1;

        protected GameSession Session;
        protected readonly CompositeDisposable Trash = new();

        protected virtual void Start()
        {
            Session = FindObjectOfType<GameSession>();
        }

        private void OnDestroy()
        {
            Trash?.Dispose();
        }

        protected void OnIndexChanged(int newValue, int _)
        {
            print($"OnIndexChanged {gameObject.name} {Index == newValue}");
            _selection.SetActive(Index == newValue);
        }

        public virtual void SetData(InventoryItemData item, int index)
        {
            Index = index;

            var definition = DefinitionsFacade.Instance.Items.Get(item.Id);
            _icon.sprite = definition.Icon;
            _value.text = definition.HasTag(ItemTag.Stackable) == true ? item.Value.ToString() : string.Empty;
        }
    }
}