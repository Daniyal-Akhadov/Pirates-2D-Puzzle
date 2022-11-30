using UnityEngine;
using Random = UnityEngine.Random;

namespace PixelCrew.Utilities
{
    public class VerticalLevitationComponent : MonoBehaviour
    {
        [SerializeField] private float _frequency = 1f;
        [SerializeField] private float _amplitude = 0.01f;
        [SerializeField] private bool _isRandomized;

        private float _time;
        private Rigidbody2D _rigidbody;
        private float _seed;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();

            if (_isRandomized == true)
                _seed = Mathf.PI * 2 * Random.value;
        }

        private void FixedUpdate()
        {
            Vector3 position = _rigidbody.position;
            position.y += Mathf.Sin(_seed + Time.time * _frequency) * _amplitude;
            _rigidbody.MovePosition(position);
        }
    }
}