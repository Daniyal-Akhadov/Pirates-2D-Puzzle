using PixelCrew.Creatures.Hero;
using UnityEngine;

namespace PixelCrew.Creatures.Core
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Creature : MonoBehaviour
    {
        protected Animator Animator;
        protected Rigidbody2D Rigidbody;
        protected AttackComponent Attacker;
        protected MovementComponent Movement;

        protected virtual void Awake()
        {
            Rigidbody = GetComponent<Rigidbody2D>();
            Animator = GetComponentInChildren<Animator>();
            Attacker = GetComponent<AttackComponent>();
            Movement = GetComponent<MovementComponent>();

            Attacker.Init(Animator);
            Movement.Init(Animator, Rigidbody);
        }
    }
}