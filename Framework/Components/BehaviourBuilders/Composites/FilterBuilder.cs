namespace Chinchillada.BehaviourSelections.BehaviourTree.Builder
{
    /// <summary>
    /// Behaviour builder for <see cref="Filter"/>.
    /// </summary>
    internal class FilterBuilder : ConditionalCompositeBuilder
    {
        /// <inheritdoc />
        protected override Composite ConstructComposite(BehaviourTree tree)
        {
            return new Filter(tree);
        }
    }
}
