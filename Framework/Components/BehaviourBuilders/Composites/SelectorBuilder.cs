namespace Chinchillada.BehaviourSelections.BehaviourTree.Builder
{
    /// <summary>
    /// Behaviour builder for <see cref="Selector"/>.
    /// </summary>
    internal class SelectorBuilder : CompositeBuilder
    {
        /// <inheritdoc />
        protected override Composite ConstructComposite(BehaviourTree tree)
        {
            return new Selector(tree);
        }
    }
}
