using UnityEngine;

namespace PixelCrew.Creatures.Core
{
    public static class CreatureAnimations
    {
        public static readonly int IsRunning = Animator.StringToHash("is_running");
        public static readonly int IsGround = Animator.StringToHash("is_ground");
        public static readonly int VerticalVelocity = Animator.StringToHash("vertical_velocity");
        public static readonly int Hit = Animator.StringToHash("hit");
        public static readonly int Attack = Animator.StringToHash("attack");
        public static readonly int IsDead = Animator.StringToHash("is_dead");
        public static readonly int Throw = Animator.StringToHash("throw");
    }
}