using System;
using UnityEngine;
using UnityEngine.Events;

namespace PixelCrew.Components.ColliderBased
{
    public class EnterCollisionComponent : MonoBehaviour
    {
        [SerializeField] private string _tag = "Player";
        [SerializeField] private UnityEvent _action;
        [SerializeField] private EnterEvent _actionWithArgument;

        private void OnCollisionEnter2D(Collision2D col)
        {
            if (col.gameObject.CompareTag(_tag))
            {
                _action?.Invoke();
                _actionWithArgument?.Invoke(col.gameObject);
            }
        }
    }

    [Serializable]
    public class EnterEvent : UnityEvent<GameObject>
    {
    }
}