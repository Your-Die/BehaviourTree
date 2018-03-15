namespace Chinchillada.BehaviourSelections.BehaviourTree.Builder
{
    /// <summary>
    /// Behaviour builder for <see cref="Sequence"/>.
    /// </summary>
    internal class SequenceBuilder : CompositeBuilder
    {
        /// <inheritdoc />
        protected override Composite ConstructComposite(BehaviourTree tree)
        {
            return new Sequence(tree);
        }
    }
}
