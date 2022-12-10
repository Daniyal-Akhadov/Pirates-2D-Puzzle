using System;
using UnityEngine;

namespace Prefabs.Creatures.Boss.AttackWays
{
    public class Bomb : MonoBehaviour
    {
        [SerializeField] private int _hitCountToExplode = 2;

        private int _hitCount;

        public void Explode()
        {
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            _hitCount++;
            // if (col.gameObject.TryGetComponent(out ))
        }
    }
}