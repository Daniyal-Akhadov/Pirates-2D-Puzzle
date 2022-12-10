using System.Collections;
using PixelCrew.Creatures.Hero.Weapons;
using PixelCrew.Utilities;
using UnityEngine;

namespace PixelCrew.Components.GameObjectBased
{
    public class RandomSpawnerWithDelay : MonoBehaviour
    {
        [SerializeField] private float _minDelay = 1f;
        [SerializeField] private float _maxDelay = 4f;

        [Space] [SerializeField] private GameObject _prefab;
        [SerializeField] private Transform[] _points;

        private bool _isSpawning;

        public void StartSpawning(float delay = 0f)
        {
            StartCoroutine(Spawn(delay));
        }

        public void StopSpawning()
        {
            _isSpawning = false;
        }

        private IEnumerator Spawn(float delay = 0f)
        {
            _isSpawning = true;
            yield return new WaitForSeconds(delay);

            while (_isSpawning == true)
            {
                var instance = SpawnUtils.Spawn(_prefab, _points[Random.Range(0, _points.Length)].position);
                instance.GetComponent<DirectionalProjectile>().Launch(Vector2.down);
                yield return new WaitForSeconds(Random.Range(_minDelay, _maxDelay));
            }
        }
    }
}