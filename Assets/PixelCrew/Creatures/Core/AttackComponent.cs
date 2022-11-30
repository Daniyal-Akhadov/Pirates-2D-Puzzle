using PixelCrew.Components;
using PixelCrew.Components.Audio;
using PixelCrew.Components.ColliderBased;
using UnityEngine;
using UnityEngine.Events;

namespace PixelCrew.Creatures.Core
{
    public class AttackComponent : MonoBehaviour
    {
        [SerializeField] private CheckOverlapComponent _range;
        [SerializeField] private UnityEvent _attack;
        [SerializeField] private UnityEvent _onAttack;

        protected Animator Animator;

        public void Init(Animator animator)
        {
            Animator = animator;
        }

        public virtual void Attack()
        {
            Animator.SetTrigger(CreatureAnimations.Attack);
            _attack?.Invoke();
        }

        public void OnAttack()
        {
            _range.Check();
            _onAttack?.Invoke();
        }
    }
}