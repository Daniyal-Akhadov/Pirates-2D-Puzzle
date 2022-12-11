using UnityEngine;
using UnityEngine.Events;

namespace PixelCrew.Utilities.ObjectPool
{
    public class PoolItem : MonoBehaviour
    {
        [SerializeField] private UnityEvent _onRestart;

        private Pool _pool;
        private int _id;

        public void Restart()
        {
            _onRestart?.Invoke();
        }

        public void Release()
        {
            _pool.Release(_id, this);
        }

        public void Retain(int id, Pool pool)
        {
            _id = id;
            _pool = pool;
        }
    }
}