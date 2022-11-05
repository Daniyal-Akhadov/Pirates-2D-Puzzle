using PixelCrew.Utilities;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

namespace PixelCrew.Components.ColliderBased
{
    public class CheckCircleOverlapComponent : MonoBehaviour
    {
        [SerializeField] private float _radius = 1f;
        [SerializeField] private LayerMask _layer;
        [SerializeField] private UnityEvent<GameObject> _onOverlap;
        private readonly Collider2D[] _results = new Collider2D[10];

        public void Check()
        {
            int size = Physics2D.OverlapCircleNonAlloc(
                transform.position,
                _radius,
                _results,
                _layer);

            for (var i = 0; i < size; i++)
            {
                _onOverlap?.Invoke(_results[i].gameObject);
            }
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Handles.color = HandlesUtilities.TransparentRed;
            Handles.DrawSolidDisc(transform.position, Vector3.forward, _radius);
        }
#endif
    }
}