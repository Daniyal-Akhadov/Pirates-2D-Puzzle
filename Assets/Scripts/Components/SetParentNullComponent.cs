using UnityEngine;

namespace PixelCrew.Components
{
    public class SetParentNullComponent : MonoBehaviour
    {
        public void SetParentNull()
        {
            transform.parent = null;
        }
    }
}