namespace Chinchillada.BehaviourSelections.BehaviorTree.Builder
{
    /// <summary>
    /// Behavior builder for <see cref="Sequence"/>.
    /// </summary>
    internal class SequenceBuilder : CompositeBuilder
    {
        /// <inheritdoc />
        protected override string TypeName => "Sequence";

        /// <inheritdoc />
        protected override Composite ConstructComposite(BehaviourTree tree)
        {
            return new Sequence(tree);
        }
    }
}
