using PixelCrew.Model;
using UnityEngine;

namespace PixelCrew.Components.GameObjectBased
{
    public class DestroyObjectComponent : MonoBehaviour
    {
        [SerializeField] private GameObject _target;
        [SerializeField] private RestoreStateComponent _state;

        public void DestroyObject()
        {
            Destroy(_target);
            if (_state != null)
                FindObjectOfType<GameSession>().StoreState(_state.Id);
        }

        public void DestroyObjectInTime(float delay)
        {
            Invoke(nameof(DestroyObject), delay);
        }
    }
}