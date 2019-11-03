using UnityEngine;

namespace PluggableAI.Scripts
{
    public abstract class Action : ScriptableObject
    {
        public abstract void Act(StateController controller);
    }
}