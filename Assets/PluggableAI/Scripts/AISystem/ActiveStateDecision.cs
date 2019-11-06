using UnityEngine;

namespace PluggableAI.Scripts
{
    [CreateAssetMenu(fileName = "ActiveState", menuName = "PluggableAI/Decisions/ActiveState")]
    public class ActiveStateDecision : Decision
    {
        public override bool Decide(StateController controller)
        {
            bool chaseTargetIsActive = controller.ChaseTarget.gameObject.activeSelf;
            return chaseTargetIsActive;
        }
    }
}