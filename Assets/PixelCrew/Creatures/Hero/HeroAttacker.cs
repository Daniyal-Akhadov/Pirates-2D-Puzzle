using PixelCrew.Components.Audio;
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
        private int SwordCount => _session.Data.Inventory.Count("Sword");

        public void Init(Animator animator, GameSession session)
        {
            Init(animator);
            _session = session;
            UpdateHeroWeapon();
        }

        public override void Attack()
        {
            if (SwordCount <= 0)
                return;

            base.Attack();
        }

        public void UpdateHeroWeapon()
        {
            Invoke("", 1f);
            Animator.runtimeAnimatorController = SwordCount > 0 ? _armed : _unarmed;
        }
    }
}