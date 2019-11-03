using UnityEngine;

namespace PluggableAI.Scripts
{
    [CreateAssetMenu(menuName = "PluggableAI/Decisions/Look")]
    internal class LookDecision : Decision
    {
        public override bool Decide(StateController controller)
        {
            var targetVisible = Look(controller);
            return targetVisible;
        }

        private bool Look(StateController controller)
        {
            RaycastHit hit;

            Debug.DrawRay(controller.eyes.position,controller.eyes.forward.normalized * controller.enemyStats.lookRange, Color.green);
            
            if (Physics.SphereCast(controller.eyes.position,
                    controller.enemyStats.lookSphereCastRadius,
                    controller.eyes.forward,
                    out hit,
                    controller.enemyStats.lookRange) && hit.collider.CompareTag("Player")
            )
            {
                controller.ChaseTarget = hit.transform;
                return true;
            }

            return false;
        }
    }
}