using UnityEngine;

namespace PluggableAI.Scripts
{
    public abstract class Decision : ScriptableObject
    {
        public abstract bool Decide(StateController controller);
    }
}