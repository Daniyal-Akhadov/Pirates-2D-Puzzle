using System;
using PixelCrew.Utilities;
using UnityEngine;

namespace PixelCrew.Components.Audio
{
    public class PlaySoundsComponent : MonoBehaviour
    {
        [SerializeField] private AudioData[] _sounds;

        private AudioSource _source;

        private void Awake()
        {
            _source = GameObject.FindWithTag(AudioUtils.SfxAudioSource).GetComponent<AudioSource>();
        }

        public void Play(string id)
        {
            foreach (var sound in _sounds)
            {
                if (sound.Id == id)
                {
                    _source.PlayOneShot(sound.Clip);
                    break;
                }
            }
        }

        [Serializable]
        private class AudioData
        {
            [SerializeField] private string _id;
            [SerializeField] private AudioClip _clip;

            public string Id => _id;

            public AudioClip Clip => _clip;
        }
    }
}