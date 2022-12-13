using PixelCrew.Components;
using UnityEngine;

namespace PixelCrew.Creatures.Boss
{
    public class BossTeleportState : StateMachineBehaviour
    {
        private TeleportPatricComponent _teleport;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            _teleport = animator.GetComponent<TeleportPatricComponent>();
            _teleport.TeleportWithPosition(animator.gameObject, _teleport.RandomDestination.position);
            _teleport.Collider.isTrigger = true;
        }
    }
}