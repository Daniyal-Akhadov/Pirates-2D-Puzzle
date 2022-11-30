using PixelCrew.Utilities;
using UnityEngine;

namespace PixelCrew.Components.ColliderBased
{
    public class CheckSquareOverlapComponent : CheckOverlapComponent
    {
        [SerializeField] private Vector2 _size = Vector2.one;
        [SerializeField] private float _angle = 1f;

        public override void Check()
        {
            int size = Physics2D.OverlapBoxNonAlloc(
                transform.position,
                _size,
                _angle,
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
            Gizmos.color = HandlesUtilities.TransparentRed;
            Gizmos.DrawCube(transform.position, _size);
        }
#endif
    }
}