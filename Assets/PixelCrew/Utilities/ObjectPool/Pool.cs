using System.Collections.Generic;
using UnityEngine;

namespace PixelCrew.Utilities.ObjectPool
{
    public class Pool : MonoBehaviour
    {
        private readonly Dictionary<int, Queue<PoolItem>> _items = new();

        private static Pool _instance;

        public static Pool Instance
        {
            get
            {
                if (_instance == null)
                {
                    var go = new GameObject("###MAIN_POOL###");
                    _instance = go.AddComponent<Pool>();
                }

                return _instance;
            }
        }

        public GameObject Get(GameObject go, Vector3 position)
        {
            GameObject result;

            var id = go.GetInstanceID();
            var queue = RequireQueue(id);

            if (queue.Count > 0)
            {
                var pooledItem = queue.Dequeue();
                pooledItem.transform.position = position;
                pooledItem.gameObject.SetActive(true);
                pooledItem.Restart();
                result = pooledItem.gameObject;
            }
            else
            {
                var instance = SpawnUtils.Spawn(go, position, gameObject.name);
                var poolItem = instance.GetComponent<PoolItem>();
                poolItem.Retain(id, this);
                result = instance;
            }

            return result;
        }

        private Queue<PoolItem> RequireQueue(int id)
        {
            if (_items.TryGetValue(id, out var queue) == false)
            {
                queue = new Queue<PoolItem>();
                _items.Add(id, queue);
            }

            return queue;
        }

        public void Release(int id, PoolItem item)
        {
            var queue = RequireQueue(id);
            queue.Enqueue(item);
            item.gameObject.SetActive(false);
        }
    }
}