using UnityEngine;
using UnityEngine.Events;

namespace PixelCrew.Creatures.Hero
{
    public class PushAwayComponent : MonoBehaviour
    {
        [SerializeField] private float _pushAwayForce = 20f;
        [SerializeField] private UnityEvent _action;

        private Rigidbody2D _rigidbody;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        public void PushAway()
        {
            Vector2 velocity = _rigidbody.velocity;
            velocity.y = 0f;
            _rigidbody.velocity = new Vector2(velocity.x, _pushAwayForce);
        }
    }
}