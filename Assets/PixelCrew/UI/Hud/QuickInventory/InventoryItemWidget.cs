using PixelCrew.Model;
using PixelCrew.Model.Definitions;
using PixelCrew.Utilities.Disposables;
using UnityEngine;
using UnityEngine.UI;

namespace PixelCrew.UI.Hud.QuickInventory
{
    public class InventoryItemWidget : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private GameObject _selection;
        [SerializeField] private Text _value;

        private int _index;

        private readonly CompositeDisposable _trash = new CompositeDisposable();

        public void SetData(InventoryItemData item, int index)
        {
            _index = index;

            var definition = DefinitionsFacade.Instance.Items.Get(item.Id);
            _icon.sprite = definition.Icon;
            _value.text = definition.IsStackable == true ? item.Value.ToString() : string.Empty;
        }
    }
}