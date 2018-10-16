namespace Chinchillada.BehaviourSelections.BehaviorTree.Builder
{
    /// <summary>
    /// Behavior builder for <see cref="Filter"/>.
    /// </summary>
    internal class FilterBuilder : ConditionalCompositeBuilder
    {
        protected override string TypeName => "Filter";

        /// <inheritdoc />
        protected override Composite ConstructComposite(BehaviourTree tree)
        {
            return new Filter(tree);
        }

    }
}
