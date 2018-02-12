﻿using UnityEngine;

namespace Chinchillada.BehaviourSelections.BehaviourTree.Builder
{
    /// <summary>
    /// Behaviour builder for <see cref="Parallel"/>.
    /// </summary>
    internal class ParallelBuilder : CompositeBuilder
    {
        /// <summary>
        /// The policy used for determining success.
        /// </summary>
        [SerializeField] private Parallel.Policy _succesPolicy;

        /// <summary>
        /// The policy used for determining failure.
        /// </summary>
        [SerializeField] private Parallel.Policy _failurePolicy;

        /// <inheritdoc />
        protected override Composite ConstructComposite(BehaviourTree tree)
        {
            return new Parallel(tree, _succesPolicy, _failurePolicy);
        }
    }
}