using System;
using UnityEngine;

namespace PluggableAI.Scripts
{
    [Serializable]
    public class Transition
    {
        [SerializeField] private Decision decision;
        [SerializeField] private State trueState;
        [SerializeField] private State falseState;

        public Decision Decision => decision;

        public State TrueState => trueState;

        public State FalseState => falseState;
    }
}