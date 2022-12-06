using System;
using PixelCrew.Model;
using PixelCrew.Model.Definitions;
using PixelCrew.Model.Definitions.Localization;
using PixelCrew.Model.Definitions.Repository;
using PixelCrew.UI.Widgets;
using PixelCrew.Utilities.Disposables;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PixelCrew.UI.Windows.Perks
{
    public class ManagerPerksWindow : AnimatedWindow
    {
        [SerializeField] private Button _buyButton;
        [SerializeField] private Button _useButton;
        [SerializeField] private ItemWidget _price;
        [SerializeField] private TMP_Text _information;
        [SerializeField] private Transform _perksContainer;

        private PredefinedDataGroup<PerkDefinition, PerkWidget> _dataGroup;
        private readonly CompositeDisposable _trash = new();
        private GameSession _session;

        private string SelectedPerk => _session.PerksModel.InterfaceSelection.Value;

        protected override void Start()
        {
            base.Start();
            _session = FindObjectOfType<GameSession>();
            _dataGroup = new PredefinedDataGroup<PerkDefinition, PerkWidget>(_perksContainer);

            _trash.Retain(_session.PerksModel.Subscribe(OnPerksChanged));
            _trash.Retain(_buyButton.onClick.Subscribe(OnBuy));
            _trash.Retain(_useButton.onClick.Subscribe(OnUse));
            
            OnPerksChanged();
        }

        private void OnPerksChanged()
        {
            _dataGroup.SetData(DefinitionsFacade.Instance.PerksRepository.All);

            string selectedId = _session.PerksModel.InterfaceSelection.Value;
            
            _useButton.gameObject.SetActive(_session.PerksModel.IsUnlocked(selectedId));
            _useButton.interactable = _session.PerksModel.Used != selectedId;
            
            _buyButton.gameObject.SetActive(_session.PerksModel.IsUnlocked(selectedId) == false);
            _buyButton.interactable = _session.PerksModel.CanBuy(selectedId);

            var selectedPerkDefinition = DefinitionsFacade.Instance.PerksRepository.Get(selectedId);
            _price.SetData(selectedPerkDefinition.Price);

            _information.text = LocalizationManager.Instance.GetLocalization(selectedPerkDefinition.Information);
        }

        private void OnDestroy()
        {
            _trash?.Dispose();
        }

        private void OnBuy()
        {
            _session.PerksModel.TryUnlock(SelectedPerk);
        }

        private void OnUse()
        {
            _session.PerksModel.UsePerk(SelectedPerk);
        }
    }
}