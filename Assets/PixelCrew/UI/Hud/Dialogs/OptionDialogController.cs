using System;
using PixelCrew.Model.Definitions.Localization;
using PixelCrew.UI.Widgets;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace PixelCrew.UI.Hud.Dialogs
{
    public class OptionDialogController : MonoBehaviour
    {
        [SerializeField] private GameObject _container;
        [SerializeField] private TMP_Text _dialog;
        [SerializeField] private Transform _optionsContainer;
        [SerializeField] private OptionItemWidget _itemWidget;

        private DataGroup<OptionData, OptionItemWidget> _dataGroup;

        private void Start()
        {
            _dataGroup = new DataGroup<OptionData, OptionItemWidget>(_itemWidget, _optionsContainer);
        }

        public void OnOptionSelected(OptionData optionData)
        {
            optionData.OnSelect?.Invoke();
            _container.SetActive(false);
        }

        public void Show(OptionDialogData dialogData)
        {
            _container.SetActive(true);
            _dialog.text = LocalizationManager.Instance.GetLocalization(dialogData.DialogText);
            _dataGroup.SetData(dialogData.Options);
        }
    }

    [Serializable]
    public class OptionDialogData
    {
        [SerializeField] private string _dialogText;
        [SerializeField] private OptionData[] _options;

        public string DialogText => _dialogText;
        public OptionData[] Options => _options;
    }

    [Serializable]
    public struct OptionData
    {
        public string Text;
        public UnityEvent OnSelect;
    }
}