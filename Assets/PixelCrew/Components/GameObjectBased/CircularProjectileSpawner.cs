using System;
using System.Collections;
using PixelCrew.Creatures.Hero.Weapons;
using PixelCrew.Utilities;
using UnityEngine;

namespace PixelCrew.Components.GameObjectBased
{
    public class CircularProjectileSpawner : MonoBehaviour
    {
        [SerializeField] private CircularProjectileSettings[] _settings;

        private Coroutine _coroutine;
        private bool _isStop;

        public int Stage { get; set; }

        public void LaunchProjectiles()
        {
            if (_isStop == true)
                return;

            _coroutine = StartCoroutine(SpawnProjectiles());
        }

        public void StopSpawning()
        {
            if (_coroutine != null)
                StopCoroutine(_coroutine);

            _isStop = true;
        }

        public void DestroyAllProjectiles()
        {
            var allProjectiles = FindObjectsOfType<DirectionalProjectile>();

            foreach (var projectile in allProjectiles)
            {
                projectile.SpriteAnimation.SetClip("destroy");
            }
        }

        private IEnumerator SpawnProjectiles()
        {
            var setting = _settings[Stage];
            var sectorStep = 2 * Mathf.PI / setting.BurstCount;

            for (int i = 0; i < setting.BurstCount; i++)
            {
                if (_isStop == true)
                    yield break;

                float angle = sectorStep * i;
                var direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));

                var instance = SpawnUtils.Spawn(setting.Prefab.gameObject, transform.position);
                instance.GetComponent<DirectionalProjectile>().Launch(direction);

                yield return new WaitForSeconds(setting.SpawnDelay);
            }

            _coroutine = null;
        }
    }

    [Serializable]
    public struct CircularProjectileSettings
    {
        [SerializeField] private DirectionalProjectile _prefab;
        [SerializeField] private int _burstCount;
        [SerializeField] private float _spawnDelay;

        public DirectionalProjectile Prefab => _prefab;

        public int BurstCount => _burstCount;

        public float SpawnDelay => _spawnDelay;
    }
}