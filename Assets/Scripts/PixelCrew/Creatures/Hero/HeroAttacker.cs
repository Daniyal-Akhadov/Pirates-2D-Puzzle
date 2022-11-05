using PixelCrew.Creatures.Core;
using PixelCrew.Model;
using UnityEditor.Animations;
using UnityEngine;

namespace PixelCrew.Creatures.Hero
{
    public class HeroAttacker : AttackComponent
    {
        [SerializeField] private AnimatorController _armed;
        [SerializeField] private AnimatorController _unarmed;

        private GameSession _session;

        public void Init(Animator animator, GameSession session)
        {
            Init(animator);
            _session = session;
            UpdateHeroWeapon();
        }

        public override void Attack()
        {
            if (_session.Data.IsArmed == false)
                return;

            base.Attack();
        }

        public void Arm()
        {
            _session.Data.IsArmed = true;
            UpdateHeroWeapon();
        }

        private void UpdateHeroWeapon()
        {
            Animator.runtimeAnimatorController = _session.Data.IsArmed ? _armed : _unarmed;
        }
    }
}