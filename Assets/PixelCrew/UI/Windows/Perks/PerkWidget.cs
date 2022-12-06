using System;
using PixelCrew.Model;
using PixelCrew.Model.Definitions.Repository;
using PixelCrew.UI.Widgets;
using UnityEngine;
using UnityEngine.UI;

namespace PixelCrew.UI.Windows.Perks
{
    public class PerkWidget : MonoBehaviour, IItemRenderer<PerkDefinition>
    {
        [SerializeField] private Image _icon;
        [SerializeField] private GameObject _isLocked;
        [SerializeField] private GameObject _isUsed;
        [SerializeField] private GameObject _isSelected;

        private GameSession _session;
        private PerkDefinition _perkDefinition;

        private void Start()
        {
            _session = FindObjectOfType<GameSession>();
            UpdateView();
        }

        public void SetData(PerkDefinition dataType, int index)
        {
            _perkDefinition = dataType;

            if (_session != null)
                UpdateView();
        }

        private void UpdateView()
        {
            _icon.sprite = _perkDefinition.Icon;
            _isUsed.SetActive(_session.PerksModel.IsUsed(_perkDefinition.Id));
            _isSelected.SetActive(_session.PerksModel.InterfaceSelection.Value == _perkDefinition.Id);
            _isLocked.SetActive(_session.PerksModel.IsUnlocked(_perkDefinition.Id));
        }

        public void OnSelect()
        {
            _session.PerksModel.InterfaceSelection.Value = _perkDefinition.Id;
        }
    }
}