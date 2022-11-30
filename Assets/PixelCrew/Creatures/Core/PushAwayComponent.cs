using PixelCrew.Utilities;
using UnityEngine;
using UnityEngine.Events;

namespace PixelCrew.Creatures.Core
{
    public class PushAwayComponent : MonoBehaviour
    {
        [SerializeField] private float _pushAwayForce = 20f;
        [SerializeField] private UnityEvent _action;

        [SerializeField] private string _tag;
        [SerializeField] private LayerMask _layer = ~0;

        private Rigidbody2D _rigidbody;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        public void PushAway(GameObject other)
        {
            if (other.IsLayerIn(_layer) == false)
                return;

            if (string.IsNullOrEmpty(_tag) == false && other.CompareTag(_tag) == false)
                return;
            
            Vector2 velocity = _rigidbody.velocity;
            velocity.y = 0f;
            _rigidbody.velocity = new Vector2(velocity.x, _pushAwayForce);
        }
    }
}