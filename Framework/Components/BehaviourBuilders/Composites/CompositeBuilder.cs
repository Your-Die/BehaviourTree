using System.Collections.Generic;

namespace Chinchillada.BehaviourSelections.BehaviourTree.Builder
{
    /// <summary>
    /// Abstract base class for <see cref="IBehaviourBuilder"/> that build <see cref="Composite"/> behaviours.
    /// </summary>
    public abstract class CompositeBuilder : ParentBuilder
    {
        /// <inheritdoc />
        public override IBehaviour Build(BehaviourTree tree)
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
        protected virtual void InitializeChildren(Composite composite, BehaviourTree tree)
        {
            IEnumerable<IBehaviour> children = BuildChildren(transform, tree);

            foreach (IBehaviour child in children)
                composite.AddChild(child);
        }
    }
}
