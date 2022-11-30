using System;
using System.Collections.Generic;
using UnityEngine;

namespace PixelCrew.Components.GameObjectBased
{
    public class ObjectsCircleMovement : MonoBehaviour
    {
        [SerializeField] private float _radius = 2f;
        [SerializeField] private float _speed = 2f;

        private float _adding;
        private float _time;
        private int _count;

        private readonly List<Transform> _children = new();
        private const float FullCircle = 2 * Mathf.PI;

        private void Awake()
        {
            _count = _children.Count;
        }

        private void OnValidate()
        {
            _children.Clear();

            for (int i = 0; i < transform.childCount; i++)
            {
                _children.Add(transform.GetChild(i));
            }

            for (var i = 0; i < _children.Count; i++)
            {
                var element = _children[i];
                float angle = i * FullCircle / _children.Count;
                element.position = GetPositionOnCircle(angle + _adding);
            }
        }

        private void Update()
        {
            _adding += Time.deltaTime;

            for (var i = 0; i < _count; i++)
            {
                var element = _children[i];

                if (element == null)
                    continue;

                float angle = i * FullCircle / _count;
                float delta = _adding * _speed;
                element.position = GetPositionOnCircle(angle + delta);
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, _radius);
        }


        private Vector3 GetPositionOnCircle(float angle)
        {
            return transform.position + new Vector3(
                Mathf.Cos(angle) * _radius,
                Mathf.Sin(angle) * _radius,
                transform.position.z);
        }
    }
}