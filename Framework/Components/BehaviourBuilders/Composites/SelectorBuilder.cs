namespace Chinchillada.BehaviourSelections.BehaviorTree.Builder
{
    /// <summary>
    /// Behavior builder for <see cref="Selector"/>.
    /// </summary>
    internal class SelectorBuilder : CompositeBuilder
    {
        /// <inheritdoc />
        protected override string TypeName => "Selector";

        /// <inheritdoc />
        protected override Composite ConstructComposite(BehaviourTree tree)
        {
            return new Selector(tree);
        }
    }
}
