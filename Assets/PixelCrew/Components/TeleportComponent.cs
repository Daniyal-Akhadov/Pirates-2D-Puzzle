using System.Collections;
using PixelCrew.Creatures.Hero;
using UnityEngine;

namespace PixelCrew.Components
{
    public class TeleportComponent : MonoBehaviour
    {
        [SerializeField] private Transform _destination;
        [SerializeField] private float _changingAlphaTime = 0.5f;
        [SerializeField] private float _moveTime;

        public void Teleport(GameObject target)
        {
            StartCoroutine(AnimateTeleport(target, _destination.position));
        }

        private IEnumerator AnimateTeleport(GameObject target, Vector3 position)
        {
            var renderer = target.GetComponentInChildren<SpriteRenderer>();
            var input = target.GetComponentInChildren<HeroInputReader>();

            if (input != null)
                input.BlockInput();

            yield return SetAlpha(renderer, 0f);
            target.SetActive(false);

            yield return SmoothlyMove(target.transform, position);

            if (input != null)
                input.AllowInput();

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