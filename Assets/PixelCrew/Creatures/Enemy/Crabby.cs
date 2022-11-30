using System.Collections;
using UnityEngine;

namespace PixelCrew.Creatures.Enemy
{
    public class Crabby : MobAI
    {
        private bool _isAttack;

        private void Update()
        {
            if (_attackRange.IsTouchingLayer == true && _isAttack == false)
            {
                StartState(Attack());
            }
        }

        protected override IEnumerator Attack()
        {
            _isAttack = true;
            var waitForSeconds = new WaitForSeconds(AttackCooldown);

            while (_attackRange.IsTouchingLayer == true)
            {
                Attacker.Attack();
                yield return waitForSeconds;
            }

            yield return new WaitForSeconds(0.6f);
            StartState(Patrol.DoPatrol());
            _isAttack = false;
        }
    }
}