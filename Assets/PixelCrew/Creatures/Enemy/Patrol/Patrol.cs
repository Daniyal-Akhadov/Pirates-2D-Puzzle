using System.Collections;
using PixelCrew.Creatures.Core;
using PixelCrew.Creatures.Hero;
using UnityEngine;

namespace PixelCrew.Creatures.Enemy.Patrol
{
    public abstract class Patrol : MonoBehaviour
    {
        [SerializeField] private float _stopTime = 1f;

        protected MovementComponent Movement;

        protected float StopTime => _stopTime;

        public abstract IEnumerator DoPatrol();

        public void Init(MovementComponent movement)
        {
            Movement = movement;
        }
    }
}