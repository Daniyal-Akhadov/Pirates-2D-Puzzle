using System;
using UnityEngine;
using UnityEngine.Events;

namespace PixelCrew.Components.GameObjectBased
{
    public class OnAwakeComponent : MonoBehaviour
    {
        [SerializeField] private UnityEvent<GameObject> _awake;

        private void Awake()
        {
            _awake?.Invoke(gameObject);
        }
    }
}