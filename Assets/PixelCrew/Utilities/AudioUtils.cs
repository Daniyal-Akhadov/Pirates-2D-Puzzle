using UnityEngine;

namespace PixelCrew.Utilities
{
    public static class AudioUtils
    {
        public const string SfxAudioSource = "SfxAudioSource";

        public static AudioSource FindSfxSource()
        {
            return GameObject.FindWithTag(SfxAudioSource).GetComponent<AudioSource>();
        }
    }
}