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

        private void OnValidate()
        {
            foreach (var setting in _settings)
            {
                if (setting.ItemPerBust > setting.BurstCount)
                {
                    setting.ItemPerBust = setting.BurstCount;
                }
            }
        }

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

            for (int i = 0; i < setting.BurstCount;)
            {
                if (_isStop == true)
                    yield break;

                for (int j = 0; j < setting.ItemPerBust; j++, i++)
                {
                    float angle = sectorStep * i;
                    var direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));

                    var instance = SpawnUtils.Spawn(setting.Prefab.gameObject, transform.position);
                    instance.GetComponent<DirectionalProjectile>().Launch(direction);

                    if (i == setting.BurstCount - 1)
                        yield break;
                }

                yield return new WaitForSeconds(setting.SpawnDelay);
            }

            _coroutine = null;
        }
    }

    [Serializable]
    public class CircularProjectileSettings
    {
        [SerializeField] private DirectionalProjectile _prefab;
        [SerializeField] private int _burstCount;
        [Range(0, 50)] [SerializeField] private int _itemPerBust;
        [SerializeField] private float _spawnDelay;

        public DirectionalProjectile Prefab => _prefab;

        public int BurstCount => _burstCount;

        public float SpawnDelay => _spawnDelay;

        public int ItemPerBust
        {
            get => _itemPerBust;
            set => _itemPerBust = value;
        }
    }
}