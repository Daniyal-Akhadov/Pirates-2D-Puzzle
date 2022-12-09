using PixelCrew.Model;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace PixelCrew.Creatures.Hero.Features
{
    public class HeroFlashLight : MonoBehaviour
    {
        [SerializeField] private float _consumePerSecond = 5f;
        [Range(0, 100)] [SerializeField] private float _percentToHide = 40f;
        [SerializeField] private Light2D _light;

        private GameSession _session;
        private float _defaultIntensity;

        private const float MinValueForTurnOff = 0.4f;

        private void Start()
        {
            _session = FindObjectOfType<GameSession>();
            _defaultIntensity = _light.intensity;
        }

        private void Update()
        {
            float consumed = Time.deltaTime * _consumePerSecond;
            float currentValue = _session.Data.Fuel.Value;

            var nextValue = currentValue - consumed;
            nextValue = Mathf.Max(nextValue, 0);
            _session.Data.Fuel.Value = nextValue;

            float progress = Mathf.Clamp(nextValue / _percentToHide, 0, 1);
            _light.intensity = Mathf.Clamp(_defaultIntensity * progress, MinValueForTurnOff, _defaultIntensity);

            if (_light.intensity <= MinValueForTurnOff)
            {
                gameObject.SetActive(false);
            }
        }
    }
}