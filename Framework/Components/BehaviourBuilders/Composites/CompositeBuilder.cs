using Chinchillada.BehaviourSelections.Utilities;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Chinchillada.BehaviourSelections.BehaviourTree.Builder
{
    /// <summary>
    /// Abstract base class for <see cref="IBehaviourBuilder"/> that build <see cref="Composite"/> behaviours.
    /// </summary>
    public abstract class CompositeBuilder : MonoBehaviour, IBehaviourBuilder
    {
        /// <inheritdoc />
        public IBehaviour Build(BehaviourTree tree)
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

        /// <summary>
        /// Builds all children found directly under <paramref name="transform"/>.
        /// </summary>
        /// <param name="transform">The transform to build the children of.</param>
        /// <param name="tree">The tree the behaviour should belong to.</param>
        /// <returns>The build behaviours.</returns>
        public static IEnumerable<IBehaviour> BuildChildren(Transform transform, BehaviourTree tree)
        {
            IEnumerable<IBehaviourBuilder> childBuilders = transform.GetComponentsInDirectChildren<IBehaviourBuilder>();
            return childBuilders.Select(child => child.Build(tree));
        }
    }
}
