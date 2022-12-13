using PixelCrew.Creatures.Core;
using PixelCrew.Creatures.Core.Health;
using PixelCrew.Model;
using PixelCrew.Model.Definitions.Player;
using UnityEngine;

namespace PixelCrew.Creatures.Hero
{
    public class HeroAttacker : AttackComponent
    {
        [SerializeField] private RuntimeAnimatorController _armed;
        [SerializeField] private RuntimeAnimatorController _unarmed;
        [SerializeField] private ModifyHealthComponent _modifyHealth;

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

            int damageWithCriticalChance = CalculateCriticalDamage(_modifyHealth.OriginalValue);
            _modifyHealth.SetValue(damageWithCriticalChance);
            base.Attack();
        }

        public void UpdateHeroWeapon()
        {
            Invoke("", 1f);
            Animator.runtimeAnimatorController = SwordCount > 0 ? _armed : _unarmed;
        }

        private int CalculateCriticalDamage(int damage)
        {
            float chance = _session.StatsModel.GetValue(StatId.CriticalDamage);

            if (Random.value * 100 <= chance)
            {
                damage *= 2;
            }

            return damage;
        }
    }
}