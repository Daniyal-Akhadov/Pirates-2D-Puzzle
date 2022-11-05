using UnityEngine;

namespace PixelCrew.Utilities
{
    public class SetParentNullComponent : MonoBehaviour
    {
        public void SetParentNull()
        {
            transform.parent = null;
        }
    }
}