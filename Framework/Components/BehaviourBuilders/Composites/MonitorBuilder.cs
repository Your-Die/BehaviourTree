using UnityEngine;

namespace Chinchillada.BehaviourSelections.BehaviourTree.Builder
{
    /// <summary>
    /// Behaviour builder for <see cref="Monitor"/>.
    /// </summary>
    internal class MonitorBuilder : ConditionalCompositeBuilder
    {
        /// <summary>
        /// The policy used for determining success.
        /// </summary>
        [SerializeField] private Parallel.Policy _succesPolicy = Parallel.Policy.RequireAll;

        /// <summary>
        /// The policy used for determining failure.
        /// </summary>
        [SerializeField] private Parallel.Policy _failurePolicy = Parallel.Policy.RequireOne;

        /// <inheritdoc />
        protected override Composite ConstructComposite(BehaviourTree tree)
        {
            return new Monitor(tree, _succesPolicy, _failurePolicy);
        }
    }
}
