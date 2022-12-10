using System;
using System.Collections;
using UnityEngine;

namespace PixelCrew.Creatures.Hero
{
    public class GetOffThePlatformComponent : MonoBehaviour
    {
        [SerializeField] private float _rayLength = 1.5f;
        [SerializeField] private LayerMask _platformLayer;

        private void OnDrawGizmos()
        {
            Debug.DrawRay(transform.position, Vector3.down * _rayLength);
        }

        public IEnumerator TryGetOff()
        {
            var hit = Physics2D.Raycast(
                transform.position,
                Vector2.down,
                _rayLength,
                _platformLayer);

            if (hit == true)
            {
                hit.collider.enabled = false;
                yield return new WaitForSeconds(0.3f);
                hit.collider.enabled = true;
            }
        }
    }
}