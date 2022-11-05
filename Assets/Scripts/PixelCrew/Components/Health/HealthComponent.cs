using System;
using UnityEngine;
using UnityEngine.Events;

namespace PixelCrew.Components.Health
{
    public class HealthComponent : MonoBehaviour
    {
        [SerializeField] private int _health;
        [SerializeField] private UnityEvent _onDamage;
        [SerializeField] private UnityEvent _onHeal;
        [SerializeField] private UnityEvent _onDie;
        [SerializeField] private HealthChangedEvent _onChanged;

        public void ModifyHealth(int delta)
        {
            _health += delta;
            _onChanged?.Invoke(_health);

            switch (delta)
            {
                case 0:
                    throw new ArgumentException("Delta == 0!");
                case < 0:
                    _onDamage?.Invoke();
                    break;
                case > 0:
                    _onHeal?.Invoke();
                    break;
            }

            if (_health <= 0)
            {
                _onDie?.Invoke();
            }
        }

        public void SetOriginHealth(int health)
        {
            if (health <= 0)
                throw new ArgumentException("argument <= 0!");

            _health = health;
        }


        [Serializable]
        public class HealthChangedEvent : UnityEvent<int>
        {
        }
    }
}