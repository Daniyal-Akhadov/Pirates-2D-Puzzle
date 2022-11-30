using UnityEngine;
using UnityEngine.Events;

namespace PixelCrew.Components.GameObjectBased
{
    public class GameObjectsContainer : MonoBehaviour
    {
        [SerializeField] private GameObject[] _gameObjects;
        [SerializeField] private UnityEvent<GameObject[]> _onDrop;

        [ContextMenu("Drop")]
        public void Drop()
        {
            _onDrop?.Invoke(_gameObjects);
        }
    }
}