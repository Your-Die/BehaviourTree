using System;
using System.Linq;

namespace Chinchillada.BehaviourSelections.BehaviorTree.Builder
{
    public abstract class DecoratorBuilder : ParentBuilder
    {
        /// <inheritdoc />
        protected override string TypeName => "Decorator";

        public override IBehavior Build(BehaviourTree tree)
        {
            IBehavior child = BuildChildren(transform, tree).First();
            return BuildDecorator(tree, child);
        }

        protected abstract IBehavior BuildDecorator(BehaviourTree tree, IBehavior child); 
    }
}
