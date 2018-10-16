using System;

namespace Chinchillada.BehaviourSelections.BehaviorTree
{
    /// <summary>
    /// A behaviour that runs in a single action and immediately returns if it was succesful or not.
    /// </summary>
    public class Action : Behavior
    {
        /// <summary>
        /// The action to take.
        /// </summary>
        private readonly Func<bool> _action;

        /// <summary>
        /// Construct a new Action behaviour.
        /// </summary>
        /// <param name="tree">The tree this behaviour belongs to.</param>
        /// <param name="action">The action.</param>
        public Action(BehaviourTree tree, Func<bool> action) : base(tree)
        {
            _action = action;
        }

        /// <summary>
        /// Runs the action once and returns the result as status.
        /// </summary>
        /// <returns>
        /// <see cref="Behavior.Status.Success"/> if the action was completed succesfully, 
        /// <see cref="Behavior.Status.Failure"/> if it failed.
        /// </returns>
        protected override Status UpdateInternal()
        {
            return _action()
                ? Status.Success
                : Status.Failure;
        }
    }
}
