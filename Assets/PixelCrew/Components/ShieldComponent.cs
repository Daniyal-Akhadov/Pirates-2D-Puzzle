using System;
using System.Collections;
using PixelCrew.Creatures.Core.Health;
using PixelCrew.Utilities.TimeManagement;
using UnityEngine;

namespace PixelCrew.Components
{
    public class ShieldComponent : MonoBehaviour
    {
        [SerializeField] private HealthComponent _health;
        [SerializeField] private Cooldown _cooldown;

        private SpriteRenderer _renderer;

        private void Awake()
        {
            _renderer = GetComponent<SpriteRenderer>();
        }

        public void TryUse()
        {
            if (_cooldown.IsReady == false || _health.IsArmed == true)
                return;

            print("I can use it");
            _renderer.enabled = true;
            _health.IsArmed = true;
            StartCoroutine(TurnOfShield(_cooldown.Value));
        }

        private IEnumerator TurnOfShield(float duration)
        {
            yield return new WaitForSeconds(duration);
            _health.IsArmed = false;
            _renderer.enabled = false;
        }
    }
}