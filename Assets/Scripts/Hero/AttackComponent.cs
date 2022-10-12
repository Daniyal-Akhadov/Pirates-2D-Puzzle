using UnityEngine;
using UnityEngine.Events;

namespace PixelCrew.Components
{
    public class AttackComponent : MonoBehaviour
    {
        [SerializeField] private int _damage = 1;
        [SerializeField] private UnityEvent _onAttack;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.TryGetComponent(out IDamageable damageable))
            {
                damageable.ApplyDamage(_damage);
                _onAttack?.Invoke();
            }
        }
    }
}