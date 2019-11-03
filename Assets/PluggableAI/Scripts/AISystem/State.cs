using UnityEngine;

namespace PluggableAI.Scripts
{
    [CreateAssetMenu(menuName = "PluggableAI/State")]
    public  class State : ScriptableObject
    {
        [SerializeField] private Action[] actions;
        [SerializeField] private Color sceneGizmoColor = Color.gray;

        [SerializeField] private Transition[] transitions;
        public Color SceneGizmoColor
        {
            get => sceneGizmoColor;
            set => sceneGizmoColor = value;
        }

        public void UpdateState(StateController controller)
        {
            DoActions(controller);
            CheckTransitions(controller);
        }

        private void DoActions(StateController controller)
        {
            foreach (var action in actions)
            {
                action.Act(controller);
            }
        }

        private void CheckTransitions(StateController controller)
        {
            foreach (var transition in transitions)
            {
                var decisionsSucceeded = transition.Decision.Decide(controller);

                // ReSharper disable once ConvertIfStatementToConditionalTernaryExpression
                if (decisionsSucceeded)
                {
                    controller.TransitionToState(transition.TrueState);
                }
                else
                {
                    controller.TransitionToState(transition.FalseState);
                }
            }
        }
    }
}