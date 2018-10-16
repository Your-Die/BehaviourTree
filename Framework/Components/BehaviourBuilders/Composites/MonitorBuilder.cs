using UnityEngine;

namespace Chinchillada.BehaviourSelections.BehaviorTree.Builder
{
    /// <summary>
    /// Behavior builder for <see cref="Monitor"/>.
    /// </summary>
    internal class MonitorBuilder : ConditionalCompositeBuilder
    {
        /// <summary>
        /// The policy used for determining success.
        /// </summary>
        [SerializeField] private Parallel.Policy _successPolicy = Parallel.Policy.RequireAll;

        /// <summary>
        /// The policy used for determining failure.
        /// </summary>
        [SerializeField] private Parallel.Policy _failurePolicy = Parallel.Policy.RequireOne;
        
        /// <inheritdoc />
        protected override string TypeName => "Monitor";

        /// <inheritdoc />
        protected override Composite ConstructComposite(BehaviourTree tree)
        {
            return new Monitor(tree, _successPolicy, _failurePolicy);
        }

    }
}
