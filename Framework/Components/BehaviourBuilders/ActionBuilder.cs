namespace Chinchillada.BehaviourSelections.BehaviorTree.Builder
{
    /// <summary>
    /// Base class for <see cref="IBehaviourBuilder"/> implementations that build <see cref="Action"/> behaviors.
    /// </summary>
    internal abstract class ActionBuilder : BehaviourBuilder
    {
        /// <inheritdoc />
        protected override string TypeName => "Action";

        /// <summary>
        /// Does the action.
        /// </summary>
        /// <returns>True if the action was successful.</returns>
        protected abstract bool DoAction();

        /// <summary>
        /// Builds the <see cref="Action"/>.
        /// </summary>
        /// <param name="tree">The tree the resulting <see cref="Action"/> is to be a part of.</param>
        /// <returns>The build <see cref="Action"/>.</returns>
        public override IBehavior Build(BehaviourTree tree)
        {
            enabled = false;
            return new Action(tree, DoAction);
        }
    }
}
