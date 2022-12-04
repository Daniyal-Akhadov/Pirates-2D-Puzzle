using PixelCrew.Model.Definitions.Localization;
using PixelCrew.UI.Widgets;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace PixelCrew.UI.Windows.LocaleWindow
{
    public class LocaleItemWidget : MonoBehaviour, IItemRenderer<LocaleInfo>
    {
        [SerializeField] private TMP_Text _text;
        [SerializeField] private GameObject _selector;
        [SerializeField] private Image _flag;
        [SerializeField] private UnityEvent<string> _selected;

        private LocaleInfo _localeInfo;

        private void Start()
        {
            LocalizationManager.Instance.OnLocaleChanged += UpdateSelection;
        }

        private void OnDestroy()
        {
            LocalizationManager.Instance.OnLocaleChanged -= UpdateSelection;
        }

        public void SetData(LocaleInfo localeInfo, int index)
        {
            _text.text = localeInfo.LocaleId.ToUpper();
            _flag.sprite = localeInfo.Icon;
            _localeInfo = localeInfo;
            UpdateSelection();
        }

        private void UpdateSelection()
        {
            bool isSelect = LocalizationManager.Instance.LocalKey == _localeInfo.LocaleId;
            _selector.SetActive(isSelect);
        }

        public void OnSelected()
        {
            _selected?.Invoke(_localeInfo.LocaleId);
        }
    }
}