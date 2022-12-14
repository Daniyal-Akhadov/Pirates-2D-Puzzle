using System;
using System.Globalization;
using PixelCrew.Model;
using PixelCrew.Model.Definitions;
using PixelCrew.Model.Definitions.Localization;
using PixelCrew.Model.Definitions.Player;
using PixelCrew.UI.Widgets;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PixelCrew.UI.Windows.PlayerStats
{
    public class StatWidget : MonoBehaviour, IItemRenderer<StatDef>
    {
        [SerializeField] private TMP_Text _name;
        [SerializeField] private Image _icon;
        [SerializeField] private ProgressBarWidget _progress;
        [SerializeField] private GameObject _selector;
        [SerializeField] private TMP_Text _currentValue;
        [SerializeField] private TMP_Text _increaseValue;

        private GameSession _session;
        private StatDef _data;

        private void Start()
        {
            _session = FindObjectOfType<GameSession>();
            UpdateView();
        }

        public void SetData(StatDef dataType, int index)
        {
            _data = dataType;

            if (_session != null)
                UpdateView();

            Debug.Log("Set data");
            Debug.Log("Some desing");
        }

        private void UpdateView()
        {
            _icon.sprite = _data.Icon;
            _name.text = LocalizationManager.Instance.GetLocalization(_data.Name);
            var statsModel = _session.StatsModel;

            var currentLevelValue = statsModel.GetValue(_data.Id);
            _currentValue.text = currentLevelValue.ToString(CultureInfo.InvariantCulture);

            var currentLevel = statsModel.GetCurrentLevel(_data.Id);
            var nextLevel = currentLevel + 1;
            var nextLevelValue = statsModel.GetValue(_data.Id, nextLevel);
            var increaseValue = nextLevelValue - currentLevelValue;
            _increaseValue.text = $"+{increaseValue}";
            _increaseValue.gameObject.SetActive(increaseValue > 0);

            int maxLevels = DefinitionsFacade.Instance.PlayerDefinitions.GetStat(_data.Id).Levels.Length - 1;
            _progress.SetProgress(currentLevel / (float)maxLevels);

            _selector.SetActive(statsModel.InterfaceSelectedStat.Value == _data.Id);
        }

        public void OnSelect()
        {
            _session.StatsModel.InterfaceSelectedStat.Value = _data.Id;
        }
    }
}