using UnityEngine;
using UnityEngine.Events;

namespace PixelCrew.Components.ColliderBased
{
    public abstract class CheckOverlapComponent : MonoBehaviour
    {
        [SerializeField] protected LayerMask Layer;
        [SerializeField] protected UnityEvent<GameObject> OnOverlap;

        protected readonly Collider2D[] Results = new Collider2D[10];

        public abstract void Check();
    }
}