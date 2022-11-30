using System.Collections;
using PixelCrew.Components.Audio;
using PixelCrew.Components.GameObjectBased;
using PixelCrew.Creatures.Core;
using PixelCrew.Model;
using PixelCrew.Utilities.TimeManagement;
using UnityEngine;

namespace PixelCrew.Creatures.Hero
{
    public class ProjectileThrower : MonoBehaviour
    {
        [SerializeField] private int _queueCount = 3;
        [SerializeField] private Cooldown _cooldown;
        [SerializeField] private SpawnComponent _projectileSpawner;

        private Animator _animator;
        private GameSession _session;
        private bool _isThrowingQueue;
        private PlaySoundsComponent _sounds;

        private int SwordCount => _session.Data.Inventory.Count("Sword");

        private const int MinProjectileCountToSave = 1;

        public void Init(Animator animator, PlaySoundsComponent sounds, GameSession session)
        {
            _animator = animator;
            _session = session;
            _sounds = sounds;
        }

        public void OnThrow()
        {
            _projectileSpawner.Spawn();
        }

        public void TryThrow()
        {
            if (_cooldown.IsReady == true && SwordCount > MinProjectileCountToSave)
            {
                Throw();
                _cooldown.Reset();
            }
        }

        public IEnumerator TryThrowQueue()
        {
            int throwCount = Mathf.Min(_queueCount, SwordCount);

            if (throwCount <= MinProjectileCountToSave)
                yield break;

            if (_isThrowingQueue == false)
            {
                var waitForSeconds = new WaitForSeconds(_cooldown.Value);
                _isThrowingQueue = true;

                for (int i = 0; i < throwCount; i++)
                {
                    Throw();
                    yield return waitForSeconds;
                }

                _isThrowingQueue = false;
            }
        }

        private void Throw()
        {
            _animator.SetTrigger(CreatureAnimations.Throw);
            _session.Data.Inventory.Remove("Sword", 1);
            _sounds.Play("Range");
        }
    }
}