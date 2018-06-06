namespace Chinchillada.BehaviourSelections.BehaviourTree.Builder
{
    public class ActiveSelectorBuilder : CompositeBuilder
    {
        protected override Composite ConstructComposite(BehaviourTree tree)
        {
            return new ActiveSelector(tree);
        }
    }
}
