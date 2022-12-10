using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace PixelCrew.Components.Effects
{
    public class ChangeLightsComponent : MonoBehaviour
    {
        [SerializeField] private Light2D[] _lights;
      [ColorUsage(true, true)]  [SerializeField] private Color _color;

        public void SetColor()
        {
            foreach (var light in _lights)
            {
                light.color = _color;
            }
        }
    }
}