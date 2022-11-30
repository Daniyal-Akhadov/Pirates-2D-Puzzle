using PixelCrew.Utilities;
using UnityEngine;

namespace PixelCrew.Components.GameObjectBased
{
    public class SpawnComponent : MonoBehaviour
    {
        [SerializeField] private GameObject _target;
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private bool _mergeToPoint;

        [ContextMenu("Spawn")]
        public void Spawn()
        {
            var newObject = SpawnUtils.Spawn(_target, _spawnPoint.position);
            newObject.transform.localScale = _spawnPoint.lossyScale;

            if (_mergeToPoint == true)
                newObject.transform.parent = _spawnPoint;
        }

        public void Spawn(Vector3 position)
        {
            var newObject = Instantiate(_target, position, Quaternion.identity);
            newObject.SetActive(true);
            newObject.transform.localScale = _spawnPoint.lossyScale;
        }
    }
}