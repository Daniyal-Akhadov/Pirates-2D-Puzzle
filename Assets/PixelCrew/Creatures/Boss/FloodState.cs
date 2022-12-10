using PixelCrew.Components.Effects;
using UnityEngine;

namespace PixelCrew.Creatures.Boss
{
    public class FloodState : StateMachineBehaviour
    {
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            var changeLight = animator.GetComponent<ChangeLightsComponent>();
            changeLight.SetColor(Color.cyan);

            var floodController = animator.GetComponent<FloodController>();
            floodController.StartFlooding(0.8f);
        }
    }
}