namespace Chinchillada.BehaviourSelections.BehaviorTree
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

        /// <summary>
        /// Construct a new <see cref="Filter"/> for the <paramref name="tree"/>.
        /// </summary>
        /// <param name="tree"></param>
        public Filter(BehaviourTree tree) : base(tree) { }

        /// <summary>
        /// Adds a new condition.
        /// </summary>
        /// <param name="condition">The condition to add.</param>
        public void AddCondition(IBehavior condition)
        {
            Children.Insert(_conditionIndex, condition);
            _conditionIndex++;
        }

        /// <inheritdoc />
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
