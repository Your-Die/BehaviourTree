using Chinchillada.BehaviourSelections.BehaviourTree;
using Chinchillada.BehaviourSelections.BehaviourTree.Builder;
using UnityEngine;

namespace Assets.BehaviourSelections.BahaviorTree.Builder
{
    /// <summary>
    /// Base class for <see cref="IBehaviourBuilder"/> implementations that build <see cref="Condition"/>.
    /// </summary>
    internal abstract class ConditionBuilder : MonoBehaviour, IBehaviourBuilder
    {
        [SerializeField] private Condition.Mode _mode = Condition.Mode.CheckOnce;

        /// <summary>
        /// The condition that will be validated by the <see cref="Condition"/> behaviour.
        /// </summary>
        /// <returns>True if the condition is valid.</returns>
        protected abstract bool ValidateCondition();

        public IBehaviour Build(BehaviourTree tree)
        {
            enabled = false;
            return new Condition(tree, ValidateCondition, _mode);
        }
    }
}
