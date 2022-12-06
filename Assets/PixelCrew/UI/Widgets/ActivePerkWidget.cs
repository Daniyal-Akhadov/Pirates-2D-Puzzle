using PixelCrew.Model;
using PixelCrew.Model.Definitions;
using PixelCrew.Model.Definitions.Repository;
using PixelCrew.Utilities.Disposables;
using UnityEngine;
using UnityEngine.UI;

namespace PixelCrew.UI.Widgets
{
    public class ActivePerkWidget : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private Image _fillIcon;

        private GameSession _session;
        private readonly CompositeDisposable _trash = new();

        private PerkDefinition CurrentPerkDef =>
            DefinitionsFacade.Instance.PerksRepository.Get(_session.PerksModel.Used);

        private float _timer;
        private bool _isPerkUsed;

        private void Start()
        {
            _session = FindObjectOfType<GameSession>();
            _session.PerksModel.Subscribe(OnPerksModelChanged);

            SetIcon(string.IsNullOrEmpty(_session.PerksModel.Used) == false ? CurrentPerkDef.Icon : null);

            _session.PerksModel.OnUsedPerk += OnUsedPerk;
        }

        private void OnUsedPerk()
        {
            CurrentPerkDef.Cooldown.Reset();
            _timer = 0f;
            _isPerkUsed = true;
        }

        private void OnDestroy()
        {
            _trash?.Dispose();
            _session.PerksModel.OnUsedPerk -= OnUsedPerk;
        }

        private void Update()
        {
            if (CurrentPerkDef == null)
                return;
            
            if (_isPerkUsed)
            {
                _isPerkUsed = false;
                CurrentPerkDef.Cooldown.Reset();
                _fillIcon.fillAmount = 1f;
                _timer = CurrentPerkDef.Cooldown.Value;
            }

            if (_fillIcon.fillAmount > 0f)
            {
                _timer -= Time.deltaTime;
                _fillIcon.fillAmount = _timer / CurrentPerkDef.Cooldown.Value;
            }
        }

        private void OnPerksModelChanged()
        {
            string usedPerk = _session.PerksModel.Used;
            SetIcon(string.IsNullOrEmpty(usedPerk) ? null : CurrentPerkDef.Icon);

            CurrentPerkDef?.Cooldown.ResetTimesUp();
        }

        private void SetIcon(Sprite sprite)
        {
            _icon.gameObject.SetActive(sprite != null);
            _fillIcon.gameObject.SetActive(sprite != null);
            _icon.sprite = sprite;
            _fillIcon.sprite = sprite;
        }
    }
}