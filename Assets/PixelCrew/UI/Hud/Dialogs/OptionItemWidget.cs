using PixelCrew.UI.Widgets;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace PixelCrew.UI.Hud.Dialogs
{
    public class OptionItemWidget : MonoBehaviour, IItemRenderer<OptionData>
    {
        [SerializeField] private TMP_Text _label;
        [SerializeField] private UnityEvent<OptionData> _onSelect;

        private OptionData _optionData;

        public void SetData(OptionData optionData, int index)
        {
            _optionData = optionData;
            _label.text = optionData.Text;
        }

        public void OnSelect()
        {
            _onSelect?.Invoke(_optionData);
        }
    }
}