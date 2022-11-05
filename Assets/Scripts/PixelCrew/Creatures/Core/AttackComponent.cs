using PixelCrew.Components;
using PixelCrew.Components.ColliderBased;
using UnityEngine;

namespace PixelCrew.Creatures.Core
{
    public class AttackComponent : MonoBehaviour
    {
        [SerializeField] private CheckCircleOverlapComponent _range;

        protected Animator Animator;

        public void Init(Animator animator)
        {
            Animator = animator;
        }

        public virtual void Attack()
        {
            Animator.SetTrigger(CreatureAnimations.Attack);
        }

        public void OnAttack()
        {
            _range.Check();
        }
    }
}