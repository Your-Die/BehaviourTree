using System.Collections.Generic;
using System.Linq;
using Chinchillada.BehaviourSelections.Utilities;
using UnityEngine;

namespace Chinchillada.BehaviourSelections.BehaviourTree.Builder
{
    public abstract class ParentBuilder : MonoBehaviour, IBehaviourBuilder
    {
        public abstract IBehaviour Build(BehaviourTree tree);

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