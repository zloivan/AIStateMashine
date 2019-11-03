using UnityEngine;

namespace PluggableAI.Scripts
{
    [CreateAssetMenu(menuName = "PluggableAI/Actions/Chase")]
    public class ChaseAction : Action
    {
        public override void Act(StateController controller)
        {
            DoChase(controller);
        }

        private void DoChase(StateController controller)
        {
            //move forward to target 
            controller.NavMeshAgent.destination = controller.ChaseTarget.position;
            controller.NavMeshAgent.isStopped = false;
        }
    }
}