using PixelCrew.Components.ColliderBased;
using PixelCrew.Utilities.TimeManagement;
using UnityEngine;

namespace PixelCrew.Creatures.Enemy
{
    public class Seashell : ShootingTrapAI
    {
        [Header("Melee")] [SerializeField] private CheckCircleOverlapComponent _meleeAttack;
        [SerializeField] private LayerCheck _canMeleeAttack;
        [SerializeField] private Cooldown _meleeCooldown;

        private static readonly int MeleeAttackKey = Animator.StringToHash("melee_attack");

        private void Update()
        {
            if (Vision.IsTouchingLayer == true)
            {
                if (_canMeleeAttack.IsTouchingLayer == true)
                {
                    if (_meleeCooldown.IsReady == true)
                    {
                        MeleeAttack();
                    }
                }
                else if (RangeCooldown.IsReady == true)
                {
                    RangeAttack();
                }
            }
        }

        public void OnMeleeAttack()
        {
            _meleeAttack.Check();
        }

        private void MeleeAttack()
        {
            Animator.SetTrigger(MeleeAttackKey);
            ResetAllCooldown();
        }

        private void ResetAllCooldown()
        {
            RangeCooldown.Reset();
            _meleeCooldown.Reset();
        }
    }
}