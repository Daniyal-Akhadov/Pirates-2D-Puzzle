using PixelCrew.Model.Data.Properties;
using UnityEngine;

namespace PixelCrew.Model
{
    [CreateAssetMenu(menuName = "Data/GameSettings", fileName = "GameSettings")]
    public class GameSettingsData : ScriptableObject
    {
        [SerializeField] private FloatPersistentProperty _music;
        [SerializeField] private FloatPersistentProperty _sfx;

        public FloatPersistentProperty Music => _music;
        public FloatPersistentProperty Sfx => _sfx;
        private static GameSettingsData _instance;

        public static GameSettingsData Instance => _instance == null ? LoadGameSettings() : _instance;

        private static GameSettingsData LoadGameSettings()
        {
            return _instance = Resources.Load<GameSettingsData>("GameSettings");
        }   

        private void OnEnable()
        {
            _music = new FloatPersistentProperty(1, SoundSettings.Music.ToString());
            _sfx = new FloatPersistentProperty(1, SoundSettings.Sfx.ToString());
        }

        private void OnValidate()
        {
            _music.Validate();
            _sfx.Validate();
        }
    }

    public enum SoundSettings
    {
        Music,
        Sfx
    }
}