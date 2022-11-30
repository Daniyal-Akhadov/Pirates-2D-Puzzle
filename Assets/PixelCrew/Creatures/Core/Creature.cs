using PixelCrew.Components.Audio;
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
        protected PlaySoundsComponent Sounds;
        
        public bool OnPlatform
        {
            get
            {
                int platform = (int)Mathf.Pow(2, LayerMask.NameToLayer("Platform"));

                return Physics2D.Raycast(transform.position,
                    Vector3.down,
                    0.5f,
                    platform);
            }
        }
        
        protected virtual void Awake()
        {
            Rigidbody = GetComponent<Rigidbody2D>();
            Animator = GetComponentInChildren<Animator>();
            Attacker = GetComponent<AttackComponent>();
            Movement = GetComponent<MovementComponent>();
            Sounds = GetComponent<PlaySoundsComponent>();

            Attacker.Init(Animator);
            Movement.Init(Animator, Rigidbody);
        }
    }
}