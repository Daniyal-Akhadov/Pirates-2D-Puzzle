using UnityEngine;

namespace PixelCrew
{
    public static class HeroAnimations
    {
        public static readonly int IsRunning = Animator.StringToHash("is_running");
        public static readonly int IsGround = Animator.StringToHash("is_ground");
        public static readonly int VerticalVelocity = Animator.StringToHash("vertical_velocity");
        public static readonly int Hit = Animator.StringToHash("hit");
    }
}