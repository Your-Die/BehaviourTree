using Chinchillada.BehaviourSelections.BehaviorTree;
using Chinchillada.BehaviourSelections.BehaviorTree.Builder;
using UnityEngine;

namespace Assets.BehaviourSelections.BehaviorTree.Builder
{
    /// <summary>
    /// Base class for <see cref="IBehaviourBuilder"/> implementations that build <see cref="Condition"/>.
    /// </summary>
    public abstract class ConditionBuilder : BehaviourBuilder
    {
        /// <summary>
        /// The <see cref="Condition.Mode"/> that the <see cref="Condition"/> should use.
        /// </summary>
        [SerializeField] private Condition.Mode _mode = Condition.Mode.CheckOnce;

        protected override string TypeName => "Condition";

        /// <summary>
        /// The condition that will be validated by the <see cref="Condition"/> behaviour.
        /// </summary>
        /// <returns>True if the condition is valid.</returns>
        protected abstract bool ValidateCondition();

        /// <summary>
        /// Builds the <see cref="Condition"/>.
        /// </summary>
        /// <param name="tree">The tree the resulting <see cref="Condition"/> is to be a part of.</param>
        /// <returns>The build <see cref="Condition"/>.</returns>
        public override IBehavior Build(BehaviourTree tree)
        {
            enabled = false;
            return new Condition(tree, ValidateCondition, _mode);
        }
    }
}
