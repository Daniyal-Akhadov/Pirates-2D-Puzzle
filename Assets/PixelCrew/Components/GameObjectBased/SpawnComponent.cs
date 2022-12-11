using PixelCrew.Utilities;
using PixelCrew.Utilities.ObjectPool;
using UnityEngine;

namespace PixelCrew.Components.GameObjectBased
{
    public class SpawnComponent : MonoBehaviour
    {
        [SerializeField] private GameObject _target;
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private bool _mergeToPoint;
        [SerializeField] private bool _usePool;

        public void SetTarget(GameObject target)
        {
            _target = target;
        }

        [ContextMenu("Spawn")]
        public void Spawn()
        {
            SpawnInstance();
        }

        public GameObject SpawnInstance()
        {
            var instance = _usePool
                ? Pool.Instance.Get(_target, _spawnPoint.position)
                : SpawnUtils.Spawn(_target, _spawnPoint.position);

            instance.transform.localScale = _spawnPoint.lossyScale;
            instance.SetActive(true);
            if (_mergeToPoint == true)
                instance.transform.parent = _spawnPoint;

            return instance;
        }
    }
}