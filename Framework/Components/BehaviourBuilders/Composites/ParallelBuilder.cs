using UnityEngine;
using UnityEngine.Serialization;

namespace Chinchillada.BehaviourSelections.BehaviorTree.Builder
{
    /// <summary>
    /// Behavior builder for <see cref="Parallel"/>.
    /// </summary>
    internal class ParallelBuilder : CompositeBuilder
    {
        /// <summary>
        /// The policy used for determining success.
        /// </summary>
        [FormerlySerializedAs("_succesPolicy")] [SerializeField] private Parallel.Policy _successPolicy;

        /// <summary>
        /// The policy used for determining failure.
        /// </summary>
        [SerializeField] private Parallel.Policy _failurePolicy;

        /// <inheritdoc />
        protected override string TypeName => "Parallel";

        /// <inheritdoc />
        protected override Composite ConstructComposite(BehaviourTree tree)
        {
            return new Parallel(tree, _successPolicy, _failurePolicy);
        }

    }
}
