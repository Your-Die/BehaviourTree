using System.Collections.Generic;
using System.Linq;
using Chinchillada.BehaviourSelections.Utilities;
using UnityEngine;

namespace Chinchillada.BehaviourSelections.BehaviorTree.Builder
{
    /// <summary>
    /// Base class for implementations of <see cref="IBehaviourBuilder"/> that build parent nodes.
    /// </summary>
    public abstract class ParentBuilder : BehaviourBuilder
    { 
        /// <summary>
        /// Builds all children found directly under <paramref name="transform"/>.
        /// </summary>
        /// <param name="transform">The transform to build the children of.</param>
        /// <param name="tree">The tree the behaviour should belong to.</param>
        /// <returns>The build behaviors.</returns>
        public static IEnumerable<IBehavior> BuildChildren(Transform transform, BehaviourTree tree)
        {
            var childBuilders = transform.GetComponentsInDirectChildren<IBehaviourBuilder>();
            return childBuilders.Select(child => child.Build(tree));
        }
    }
}