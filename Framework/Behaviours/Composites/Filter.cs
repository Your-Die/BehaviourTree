namespace Chinchillada.BehaviourSelections.BehaviourTree
{
    /// <summary>
    /// A sequence behaviour that first forces its conditions before running its actions.
    /// </summary>
    public class Filter : Sequence, IConditionalComposite
    {
        /// <summary>
        /// The index of the newest added condition.
        /// Used to insert new conditions, to preserve the order of added conditions but make them
        /// appear before the actions.
        /// </summary>
        private int _conditionIndex;

        public Filter(BehaviourTree tree) : base(tree) { }

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
