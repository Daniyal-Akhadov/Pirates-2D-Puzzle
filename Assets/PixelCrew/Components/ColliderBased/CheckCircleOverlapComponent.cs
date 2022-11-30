using PixelCrew.Utilities;
using UnityEditor;
using UnityEngine;

namespace PixelCrew.Components.ColliderBased
{
    public class CheckCircleOverlapComponent : CheckOverlapComponent
    {
        [SerializeField] private float _radius = 1f;

        public override void Check()
        {
            int size = Physics2D.OverlapCircleNonAlloc(
                transform.position,
                _radius,
                Results,
                Layer);

            for (var i = 0; i < size; i++)
            {
                OnOverlap?.Invoke(Results[i].gameObject);
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