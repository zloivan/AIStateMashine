
using UnityEngine;

namespace PluggableAI.Scripts
{
    [CreateAssetMenu(menuName = "PluggableAI/Actions/Patrol")]
    internal class PatrolAction : Action
    {
        public override void Act(StateController controller)
        {
            Patrol(controller);
        }

        private void Patrol(StateController controller)
        {
            controller.NavMeshAgent.destination = controller.WayPointList[controller.NextWayPoint].position;
            controller.NavMeshAgent.isStopped = false;

            if (controller.NavMeshAgent.remainingDistance <= controller.NavMeshAgent.stoppingDistance
                && controller.NavMeshAgent.pathPending == false)
            {
                controller.NextWayPoint = (controller.NextWayPoint + 1) % controller.WayPointList.Count;
            }
        }
    }
}