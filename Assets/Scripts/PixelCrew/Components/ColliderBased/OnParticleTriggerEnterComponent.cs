using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.ParticleSystem;

namespace PixelCrew.Components.ColliderBased
{
    public class OnParticleTriggerEnterComponent : MonoBehaviour
    {
        [SerializeField] private float _delay = 0f;
        [SerializeField] private ParticleSystem _particle;
        [SerializeField] private UnityEvent _action;
        [SerializeField] private UnityEvent<Vector3> _coinTriggerEnter;

        private bool _isStarted;
        private bool _isWork;
        private float _timer;

        private void Awake()
        {
            _timer = _delay;
        }

        private void Update()
        {
            if (_isStarted == true)
            {
                _timer -= Time.deltaTime;

                if (_timer <= 0f)
                {
                    _isWork = true;
                }
            }
        }

        private void OnParticleTrigger()
        {
            _isStarted = true;

            if (_isWork == false)
                return;

            var particles = new List<Particle>();
            _particle.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, particles);

            for (var i = 0; i < particles.Count; i++)
            {
                _action?.Invoke();
                var particle = particles[i];
                particle.remainingLifetime = 0f;
                particles[i] = particle;
                _coinTriggerEnter?.Invoke(particle.position);
            }

            _particle.SetTriggerParticles(ParticleSystemTriggerEventType.Enter, particles);
        }

        public void ResetDelay()
        {
            _isStarted = false;
            _isWork = false;
            _timer = _delay;
        }
    }
}