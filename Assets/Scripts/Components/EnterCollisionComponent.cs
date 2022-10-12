﻿using System;
using UnityEngine;
using UnityEngine.Events;

namespace PixelCrew.Components
{
    public class EnterCollisionComponent : MonoBehaviour
    {
        [SerializeField] private string _tag = "Player";
        [SerializeField] private EnterEvent _action;

        private void OnCollisionEnter2D(Collision2D col)
        {
            if (col.gameObject.CompareTag(_tag))
                _action?.Invoke(col.gameObject);
        }
    }

    [Serializable]
    public class EnterEvent : UnityEvent<GameObject>
    {
    }
}