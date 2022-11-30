using UnityEngine;

namespace PixelCrew.Utilities
{
    public static class GameObjectExtensions
    {
        public static bool IsLayerIn(this GameObject gameObject, LayerMask layer)
        {
            return (layer.value & (1 << gameObject.layer)) != 0;
        }
    }
}