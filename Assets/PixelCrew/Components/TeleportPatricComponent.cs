using System.Collections;
using PixelCrew.Creatures.Hero;
using UnityEngine;

namespace PixelCrew.Components
{
    public class TeleportPatricComponent : MonoBehaviour
    {
        [SerializeField] private Transform[] _destinations;
        [SerializeField] private float _changingAlphaTime = 0.5f;
        [SerializeField] private float _moveTime;
        [SerializeField] private ParticleSystem _effect;
        [SerializeField] private float _teleportDelay;

        private Collider2D _collider;
        private Rigidbody2D _rigidbody;
        private Vector3 _originalPosition;

        public Transform RandomDestination
        {
            get
            {
                Transform result;

                do
                {
                    result = _destinations[Random.Range(0, _destinations.Length - 1)];
                } while (result.position == transform.position);

                return result;
            }
        }

        public Collider2D Collider => _collider;

        private void Awake()
        {
            _originalPosition = transform.position;
            _collider = GetComponent<Collider2D>();
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        public void TeleportWithPosition(GameObject target, Vector3 position)
        {
            StartCoroutine(AnimateTeleport(target, position));
            Instantiate(_effect, position, Quaternion.identity);
        }

        public void ReturnToOriginalPoint(GameObject target)
        {
            TeleportWithPosition(target, _originalPosition);
        }

        private IEnumerator AnimateTeleport(GameObject target, Vector3 position)
        {
            var renderer = target.GetComponent<SpriteRenderer>();

            yield return new WaitForSeconds(_teleportDelay);
            yield return SetAlpha(renderer, 0f);
            _rigidbody.simulated = false;
            yield return SmoothlyMove(target.transform, position);
            yield return SetAlpha(renderer, 1f);
            _rigidbody.simulated = true;
        }

        private IEnumerator SetAlpha(SpriteRenderer renderer, float finalAlpha)
        {
            float timer = 0f;
            float startAlpha = renderer.color.a;

            while (timer <= _changingAlphaTime)
            {
                timer += Time.deltaTime;
                Color color = renderer.color;
                color.a = Mathf.MoveTowards(startAlpha, finalAlpha, timer / _changingAlphaTime);
                renderer.color = color;
                yield return null;
            }
        }

        private IEnumerator SmoothlyMove(Transform target, Vector3 destination)
        {
            float timer = 0f;

            while (target.position != destination)
            {
                timer += Time.deltaTime;
                target.position = Vector3.MoveTowards(target.position, destination, timer / _moveTime);
                yield return null;
            }
        }
    }
}