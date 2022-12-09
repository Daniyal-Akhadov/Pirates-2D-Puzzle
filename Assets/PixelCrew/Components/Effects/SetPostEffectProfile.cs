using UnityEngine;
using UnityEngine.Rendering;

namespace PixelCrew.Components.Effects
{
    public class SetPostEffectProfile : MonoBehaviour
    {
        [SerializeField] private VolumeProfile _target;

        public void Set()
        {
            var all = FindObjectsOfType<Volume>();

            foreach (var volume in all)
            {
                if (volume.isGlobal == false)
                    continue;

                volume.profile = _target;
            }
        }
    }
}