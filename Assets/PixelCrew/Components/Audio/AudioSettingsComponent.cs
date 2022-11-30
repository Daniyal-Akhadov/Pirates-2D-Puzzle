using System;
using PixelCrew.Model;
using PixelCrew.Model.Data.Properties;
using UnityEngine;

namespace PixelCrew.Components.Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioSettingsComponent : MonoBehaviour
    {
        [SerializeField] private SoundSettings _mode;

        private FloatPersistentProperty model;
        private AudioSource _source;

        private void Awake()
        {
            _source = GetComponent<AudioSource>();
        }

        private void Start()
        {
            model = FindProperty();
            model.OnChanged += OnSoundSettingsChanged;
            OnSoundSettingsChanged(model.Value, model.Value);
        }

        private void OnDestroy()
        {
            model.OnChanged -= OnSoundSettingsChanged;
        }

        private void OnSoundSettingsChanged(float newValue, float oldValue)
        {
            _source.volume = newValue;
        }

        private FloatPersistentProperty FindProperty()
        {
            switch (_mode)
            {
                case SoundSettings.Music:
                    return GameSettingsData.Instance.Music;
                case SoundSettings.Sfx:
                    return GameSettingsData.Instance.Sfx;
            }

            throw new ArgumentException("Undefined mode!");
        }
    }
}