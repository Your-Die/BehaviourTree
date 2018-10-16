using UnityEngine.Events;

namespace Chinchillada.BehaviourSelections.BehaviorTree.Builder
{
    /// <summary>
    /// <see cref="ActionBuilder"/> that delegates the calling of the action behaviour to a <see cref="UnityEvent"/>.
    /// </summary>
    internal class UnityActionBuilder : ActionBuilder
    {
        /// <summary>
        /// The action.
        /// </summary>
        public UnityEvent Action;

        /// <inheritdoc />
        protected override bool DoAction()
        {
            Action.Invoke();
            return true;
        }
    }
}
