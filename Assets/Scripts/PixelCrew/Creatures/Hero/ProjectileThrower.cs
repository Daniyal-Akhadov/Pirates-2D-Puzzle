using System.Collections;
using PixelCrew.Components;
using PixelCrew.Components.GameObjectBased;
using PixelCrew.Creatures.Core;
using PixelCrew.Model;
using PixelCrew.Utilities;
using PixelCrew.Utilities.TimeManagement;
using UnityEngine;

namespace PixelCrew.Creatures.Hero
{
    public class ProjectileThrower : MonoBehaviour
    {
        [SerializeField] private int _count = 3;
        [SerializeField] private Cooldown _cooldown;
        [SerializeField] private SpawnComponent _projectileSpawner;

        private Animator _animator;
        private GameSession _session;
        private int _currentCount;
        private bool _isThrowQueue;

        private const int MinProjectileCount = 1;

        public void Init(Animator animator, GameSession session)
        {
            _animator = animator;
            _session = session;
            _currentCount = _session.Data.Projectiles;
        }

        public void OnThrow()
        {
            _projectileSpawner.Spawn();
        }

        public void TryThrow()
        {
            if (_cooldown.IsReady == true && _currentCount > MinProjectileCount)
            {
                Throw();
                _cooldown.Reset();
            }
        }

        public IEnumerator TryThrowQueue()
        {
            if (_currentCount > _count && _isThrowQueue == false)
            {
                var waitForSeconds = new WaitForSeconds(_cooldown.Value);
                _isThrowQueue = true;

                for (int i = 0; i < _count; i++)
                {
                    Throw();
                    yield return waitForSeconds;
                }

                _isThrowQueue = false;
            }
        }

        private void Throw()
        {
            _animator.SetTrigger(CreatureAnimations.Throw);
            _currentCount--;
            SaveData();
        }

        private void SaveData()
        {
            _session.Data.Projectiles = _currentCount;
        }
    }
}