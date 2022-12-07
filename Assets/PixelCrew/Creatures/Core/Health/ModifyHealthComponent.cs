using System;
using UnityEngine;

namespace PixelCrew.Creatures.Core.Health
{
    public class ModifyHealthComponent : MonoBehaviour
    {
        [SerializeField] private int _value;

        public int OriginalValue { get; private set; }

        private void Start()
        {
            OriginalValue = _value;
        }

        public void Apply(GameObject target)
        {
            var healthComponent = target.GetComponent<HealthComponent>();

            if (healthComponent != null)
            {
                healthComponent.ModifyHealth(gameObject, _value);
            }
        }

        public void SetValue(int value)
        {
            _value = value;
        }
    }
}