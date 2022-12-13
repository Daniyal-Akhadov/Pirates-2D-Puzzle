using PixelCrew.Components;
using UnityEngine;

namespace PixelCrew.Creatures.Boss
{
    public class BossComeBackToOriginPointState : StateMachineBehaviour
    {
        private TeleportPatricComponent _teleport;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            _teleport = animator.GetComponent<TeleportPatricComponent>();
            _teleport.ReturnToOriginalPoint(animator.gameObject);
            _teleport.Collider.isTrigger = true;
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            _teleport.Collider.isTrigger = false;
        }
    }
}