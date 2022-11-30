using System;
using PixelCrew.Utilities;
using UnityEngine;

namespace PixelCrew.Components.ColliderBased
{
    public class StayTriggerComponent : MonoBehaviour
    {
        [SerializeField] private string _tag;
        [SerializeField] private LayerMask _layer = ~0;
        [SerializeField] private EnterEvent _stay;
        [SerializeField] private EnterEvent _exit;

        private void OnTriggerStay2D(Collider2D other)
        {
            if (other.gameObject.IsLayerIn(_layer) == false)
                return;

            if (string.IsNullOrEmpty(_tag) == false && other.CompareTag(_tag) == false)
                return;

            _stay?.Invoke(other.gameObject);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.IsLayerIn(_layer) == false)
                return;

            if (string.IsNullOrEmpty(_tag) == false && other.CompareTag(_tag) == false)
                return;

            _exit?.Invoke(other.gameObject);
        }
    }
}