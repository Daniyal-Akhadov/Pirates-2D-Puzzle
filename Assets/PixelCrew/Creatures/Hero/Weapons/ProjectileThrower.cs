using System.Collections;
using PixelCrew.Components.Audio;
using PixelCrew.Components.GameObjectBased;
using PixelCrew.Creatures.Core;
using PixelCrew.Model;
using PixelCrew.Model.Data;
using PixelCrew.Model.Definitions;
using PixelCrew.Model.Definitions.Repository.Items;
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

        private int SwordCount => _session.Data.Inventory.Count(Sword);

        private InventoryItemData SelectedItem => _session.QuickInventory.SelectedItem;

        private const string Sword = "Sword";

        private bool CanThrow
        {
            get
            {
                var definition = DefinitionsFacade.Instance.Items.Get(SelectedItem.Id);

                if (definition.Id == Sword)
                {
                    return SwordCount > MinProjectileCountToSave;
                }

                return definition.HasTag(ItemTag.Throwable);
            }
        }

        private const int MinProjectileCountToSave = 1;

        public void Init(Animator animator, PlaySoundsComponent sounds, GameSession session)
        {
            _animator = animator;
            _session = session;
            _sounds = sounds;
        }

        public void OnThrow()
        {
            print("Throw");
            var throwableId = _session.QuickInventory.SelectedItem.Id;
            var throwableDefinition = DefinitionsFacade.Instance.ThrowableRepository.Get(throwableId);
            _projectileSpawner.SetTarget(throwableDefinition.Projectile);
            _projectileSpawner.Spawn();
            _session.Data.Inventory.Remove(throwableId, 1);
            _sounds.Play("Range");
        }

        public void TryThrow()
        {
            if (_cooldown.IsReady == true && CanThrow)
            {
                Throw();
                _cooldown.Reset();
            }
        }

        public IEnumerator TryThrowQueue()
        {
            var selectedCount = _session.Data.Inventory.Count(SelectedItem.Id);
            int throwCount = Mathf.Min(_queueCount, selectedCount);

            if (throwCount <= MinProjectileCountToSave && SelectedItem.Id == Sword)
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
        }
    }
}