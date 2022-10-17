using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PixelCrew.Components
{
    public class TeleportComponent : MonoBehaviour
    {
        [SerializeField] private Transform _destination;
        [SerializeField] private float _changingAlphaTime = 0.5f;
        [SerializeField] private float _moveTime;

        public void Teleport(GameObject target)
        {
            StartCoroutine(AnimateTeleport(target));
        }

        private IEnumerator AnimateTeleport(GameObject target)
        {
            var renderer = target.GetComponentInChildren<SpriteRenderer>();
            var input = target.GetComponent<HeroInputReader>();

            SetLock(input, true);
            yield return SetAlpha(renderer, 0f);
            target.SetActive(false);

            yield return SmoothlyMove(target.transform);

            SetLock(input, false);
            target.SetActive(true);
            yield return SetAlpha(renderer, 1f);
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

        private IEnumerator SmoothlyMove(Transform target)
        {
            float timer = 0f;

            while (timer <= _moveTime)
            {
                timer += Time.deltaTime;
                target.position = Vector3.MoveTowards(target.position, _destination.position, timer / _moveTime);
                yield return null;
            }
        }

        private void SetLock(HeroInputReader input, bool isLocked)
        {
            input.enabled = !isLocked;
        }
    }
}