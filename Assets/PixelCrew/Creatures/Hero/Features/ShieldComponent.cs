using System.Collections;
using PixelCrew.Creatures.Core.Health;
using PixelCrew.Utilities.TimeManagement;
using UnityEngine;

namespace PixelCrew.Creatures.Hero.Features
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

            if (_renderer != null) _renderer.enabled = true;
            _health.IsArmed = true;
            StartCoroutine(TurnOfShield(_cooldown.Value));
        }

        private IEnumerator TurnOfShield(float duration)
        {
            yield return new WaitForSeconds(duration);
            _health.IsArmed = false;
            if (_renderer != null) _renderer.enabled = false;
        }
    }
}