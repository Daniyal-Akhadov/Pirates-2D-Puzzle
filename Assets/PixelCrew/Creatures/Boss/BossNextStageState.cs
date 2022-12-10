using PixelCrew.Components.Effects;
using PixelCrew.Components.GameObjectBased;
using UnityEngine;

namespace Prefabs.Creatures.Boss.Patric
{
    public class BossNextStageState : StateMachineBehaviour
    {
        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            var spawner = animator.GetComponent<CircularProjectileSpawner>();
            spawner.Stage++;

            var changeLight = animator.GetComponent<ChangeLightsComponent>();
            changeLight.SetColor();

            var bombsSpawner = animator.GetComponent<RandomSpawnerWithDelay>();
            bombsSpawner.StartSpawning(1f);
        }
    }
}