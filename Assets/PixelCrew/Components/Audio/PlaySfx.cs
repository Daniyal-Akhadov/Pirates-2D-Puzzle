using PixelCrew.Utilities;
using UnityEngine;

namespace PixelCrew.Components.Audio
{
    public class PlaySfx : MonoBehaviour
    {
        [SerializeField] private AudioClip _clip;

        private AudioSource _source;

        private void Awake()
        {
            _source = GameObject.FindWithTag(AudioUtils.SfxAudioSource).GetComponent<AudioSource>();
        }

        public void Play()
        {
            _source.PlayOneShot(_clip);
        }
    }
}