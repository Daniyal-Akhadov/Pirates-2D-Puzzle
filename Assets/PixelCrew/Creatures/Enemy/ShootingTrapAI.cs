using PixelCrew.Components.ColliderBased;
using PixelCrew.Components.GameObjectBased;
using PixelCrew.Utilities.TimeManagement;
using UnityEngine;

namespace PixelCrew.Creatures.Enemy
{
    public class ShootingTrapAI : MonoBehaviour
    {
        [SerializeField] protected LayerCheck Vision;

        [Header("Range")] [SerializeField] private SpawnComponent _rangeAttack;
        [SerializeField] protected Cooldown RangeCooldown;

        protected Animator Animator;

        private static readonly int RangeAttackKey = Animator.StringToHash("range_attack");

        private void Awake()
        {
            Animator = GetComponentInChildren<Animator>();
        }

        private void Update()
        {
            if (Vision.IsTouchingLayer == true)
            {
                if (RangeCooldown.IsReady == true)
                {
                    RangeAttack();
                }
            }
        }

        public void OnRangeAttack()
        {
            _rangeAttack.Spawn();
        }

        public void RangeAttack()
        {
            Animator.SetTrigger(RangeAttackKey);
            RangeCooldown.Reset();
        }
    }
}