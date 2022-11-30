using PixelCrew.Model.Data.Properties;
using PixelCrew.Utilities.Disposables;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PixelCrew.UI.Widgets
{
    public class AudioSettingsWidget : MonoBehaviour
    {
        [SerializeField] private Slider _slider;
        [SerializeField] private TMP_Text _value;

        private FloatPersistentProperty _model;
        private readonly CompositeDisposable _trash = new CompositeDisposable();

        private void Start()
        {
            _trash.Retain(_slider.onValueChanged.Subscribe(OnSliderValueChanged));
        }

        public void SetModel(FloatPersistentProperty model)
        {
            _model = model;
            model.Subscribe(OnValueChanged);
            OnValueChanged(model.Value, model.Value);
        }

        private void OnSliderValueChanged(float value)
        {
            _model.Value = value;
        }

        private void OnValueChanged(float newValue, float oldValue)
        {
            var textValue = Mathf.RoundToInt(newValue * 100);
            _value.text = textValue.ToString("000");
            _slider.normalizedValue = newValue;
        }

        private void OnDestroy()
        {
            _trash.Dispose();
        }
    }
}