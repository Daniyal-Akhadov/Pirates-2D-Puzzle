using UnityEngine;

namespace PixelCrew.Components.Effects.CameraRelated
{
    public class DoCameraShakeComponent : MonoBehaviour
    {
        private CameraShakeEffect _effect;

        private void Start()
        {
            _effect = FindObjectOfType<CameraShakeEffect>();
        }

        public void Do()
        {
            _effect.TryShake();
        }
    }
}