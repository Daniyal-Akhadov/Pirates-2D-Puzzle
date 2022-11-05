using UnityEngine;

namespace PixelCrew.Components.GameObjectBased
{
    public class DestroyObjectComponent : MonoBehaviour
    {
        [SerializeField] private GameObject _target;

        public void DestroyObject()
        {
            Destroy(_target);
        }
    }
}