namespace Chinchillada.BehaviourSelections.BehaviourTree
{
    /// <summary>
    /// A parralel composite behaviour that monitors conditions in parralel to the actions being performed each update.
    /// </summary>
    public class Monitor : Parallel, IConditionalComposite
    {
        private int _conditionIndex;

        public Monitor(BehaviourTree tree, Policy successPolicy, Policy failurePolicy = Policy.RequireOne) 
            : base(tree, successPolicy, failurePolicy) { }
        
        /// <summary>
        /// Adds a new condition.
        /// </summary>
        /// <param name="condition">The condition to add.</param>
        public void AddCondition(IBehaviour condition)
        {
            Children.Insert(_conditionIndex, condition);
            _conditionIndex++;
        }

        /// <summary>
        /// Adds a new action.
        /// </summary>
        /// <param name="action">The action to add.</param>
        public void AddAction(IBehaviour action)
        {
            Children.Add(action);
        }
    }
}
