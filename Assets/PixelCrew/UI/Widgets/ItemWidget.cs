using PixelCrew.Model.Definitions;
using PixelCrew.Model.Definitions.Repository;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PixelCrew.UI.Widgets
{
    public class ItemWidget : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private TMP_Text _text;

        public void SetData(ItemWithCount price)
        {
            var definitionItem = DefinitionsFacade.Instance.Items.Get(price.ItemId);
            _icon.sprite = definitionItem.Icon;
            _text.text = price.Count.ToString();
        }
    }
}