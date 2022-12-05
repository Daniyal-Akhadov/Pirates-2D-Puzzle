using PixelCrew.Model;
using UnityEngine;

namespace PixelCrew.Components
{
    public class RestoreStateComponent : MonoBehaviour
    {
        [SerializeField] private string _id;

        private GameSession _session;

        public string Id => _id;

        private void Start()
        {
            _session = FindObjectOfType<GameSession>();
            if (_session.IsDefaultCheckPoint == true)
                return;

            var isDestroyed = _session.RestoreState(_id);

            if (isDestroyed == true)
                Destroy(gameObject);
        }
    }
}