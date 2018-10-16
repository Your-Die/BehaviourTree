namespace Chinchillada.BehaviourSelections.BehaviorTree
{
    /// <summary>
    /// A <see cref="Parallel"/> composite behaviour that monitors conditions in parallel to the actions being performed each update.
    /// </summary>
    public class Monitor : Parallel, IConditionalComposite
    {
        /// <summary>
        /// The index of the newest added condition.
        /// Used to insert new conditions, to preserve the order of added conditions but make them
        /// appear before the actions.
        /// </summary>
        private int _conditionIndex;

        /// <summary>
        /// Construct a new Monitor composite behaviour.
        /// </summary>
        /// <param name="tree">The tree this should be a node of.</param>
        /// <param name="successPolicy">The policy for determining success.</param>
        /// <param name="failurePolicy">The policy for determining failure.</param>
        public Monitor(BehaviourTree tree, Policy successPolicy, Policy failurePolicy = Policy.RequireOne) 
            : base(tree, successPolicy, failurePolicy) { }
        
        /// <summary>
        /// Adds a new condition.
        /// </summary>
        /// <param name="condition">The condition to add.</param>
        public void AddCondition(IBehavior condition)
        {
            Children.Insert(_conditionIndex, condition);
            _conditionIndex++;
        }

        /// <summary>
        /// Adds a new action.
        /// </summary>
        /// <param name="action">The action to add.</param>
        public void AddAction(IBehavior action)
        {
            Children.Add(action);
        }
    }
}
