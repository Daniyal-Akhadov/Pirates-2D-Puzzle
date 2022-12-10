using PixelCrew.Components.GameObjectBased;
using UnityEngine;

namespace Prefabs.Creatures.Boss.Patric
{
    public class BossShootState : StateMachineBehaviour
    {
        [SerializeField] private bool _twiceShoot;

        private CircularProjectileSpawner _spawner;
        private RandomSpawnerWithDelay _bombsSpawner;

        private bool _isSpawnedTwice;
        private float _timer;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            _timer = 0f;
            _isSpawnedTwice = false;
            
            _spawner = animator.GetComponent<CircularProjectileSpawner>();
            _spawner.LaunchProjectiles();
            
   
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (_twiceShoot == false)
                return;

            _timer += Time.deltaTime;

            if (_isSpawnedTwice == false && _timer > stateInfo.length / 1.5f)
            {
                _isSpawnedTwice = true;
                _spawner.LaunchProjectiles();
            }
        }
    }
}