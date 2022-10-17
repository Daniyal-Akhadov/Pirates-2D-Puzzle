using UnityEngine;

namespace PixelCrew.Components
{
    public class SpawnComponent : MonoBehaviour
    {
        [SerializeField] private GameObject _target;
        [SerializeField] private Transform _spawnPoint;

        [ContextMenu("Spawn")]
        public void Spawn()
        {
            var newObject = Instantiate(_target, _spawnPoint.position, Quaternion.identity);
            newObject.transform.localScale = _spawnPoint.lossyScale;
        }

        public void Spawn(Vector3 position)
        {
            var newObject = Instantiate(_target, position, Quaternion.identity);
            newObject.SetActive(true);
            newObject.transform.localScale = _spawnPoint.lossyScale;
        }
    }
}