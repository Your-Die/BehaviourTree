using System;
using System.Linq;

namespace Chinchillada.BehaviourSelections.BehaviourTree.Builder
{
    public abstract class DecoratorBuilder : ParentBuilder
    {
        public override IBehaviour Build(BehaviourTree tree)
        {
            IBehaviour child = BuildChildren(transform, tree).First();
            return BuildDecorator(tree, child);
        }

        protected abstract IBehaviour BuildDecorator(BehaviourTree tree, IBehaviour child); 
    }
}
