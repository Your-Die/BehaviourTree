using System.Collections.Generic;
using Chinchillada.BehaviourSelections.Utilities;

namespace Chinchillada.BehaviourSelections.BehaviorTree.Builder
{
    /// <summary>
    /// Abstract base class for <see cref="IBehaviourBuilder"/> that build <see cref="Composite"/> behaviors.
    /// </summary>
    public abstract class CompositeBuilder : ParentBuilder
    {
        /// <inheritdoc />
        public override IBehavior Build(BehaviourTree tree)
        {
            Composite composite = ConstructComposite(tree);
            InitializeChildren(composite, tree);

            return composite;
        }

        /// <summary>
        /// Constructs the composite implementation.
        /// </summary>
        /// <returns>The composite.</returns>
        protected abstract Composite ConstructComposite(BehaviourTree tree);

        /// <summary>
        /// Initializes the children for the <paramref name="composite"/>.
        /// </summary>
        /// <param name="composite">The composite we want to initialize the children for.</param>
        /// <param name="tree">The tree this <see cref="IBehaviourBuilder"/> is building it's behaviour.</param>
        protected virtual void InitializeChildren(Composite composite, BehaviourTree tree)
        {
            //Build and register the children.
            IEnumerable<IBehavior> children = BuildChildren(transform, tree);
            children.ForEach(composite.AddChild);
        }
    }
}
