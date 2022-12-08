using UnityEngine;

namespace PixelCrew.Components
{
    public class DashComponent : MonoBehaviour
    {
        [SerializeField] private float _impulse = 9000f;

        private Rigidbody2D _rigidbody;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        public void Use()
        {
            _rigidbody.velocity = Vector2.zero;
            print(_impulse * Vector2.right * transform.lossyScale.x);
            _rigidbody.AddForce(_impulse * Vector2.right * transform.lossyScale.x, ForceMode2D.Force);
        }
    }
}