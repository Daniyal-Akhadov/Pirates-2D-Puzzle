using PixelCrew.Utilities;
using UnityEngine;

namespace PixelCrew.Components.GameObjectBased
{
    public class SpawnComponent : MonoBehaviour
    {
        [SerializeField] private GameObject _target;
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private bool _mergeToPoint;

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
            var newObject = SpawnUtils.Spawn(_target, _spawnPoint.position);
            newObject.transform.localScale = _spawnPoint.lossyScale;
            newObject.SetActive(true);
            if (_mergeToPoint == true)
                newObject.transform.parent = _spawnPoint;

            return newObject;
        }
    }
}