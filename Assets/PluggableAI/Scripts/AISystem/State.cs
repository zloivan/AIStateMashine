using UnityEngine;

namespace PluggableAI.Scripts
{
    [CreateAssetMenu(menuName = "PluggableAI/State")]
    public  class State : ScriptableObject
    {
        [SerializeField] private Action[] actions;
        [SerializeField] private Color sceneGizmoColor = Color.gray;

        public Color SceneGizmoColor
        {
            get => sceneGizmoColor;
            set => sceneGizmoColor = value;
        }

        public void UpdateState(StateController controller)
        {
            DoActions(controller);
        }

        private void DoActions(StateController controller)
        {
            foreach (var action in actions)
            {
                action.Act(controller);
            }
        }
    }
}