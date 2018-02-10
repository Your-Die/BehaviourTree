namespace Chinchillada.BehaviourSelections.BehaviourTree
{
    public interface IConditionalComposite
    {
        /// <summary>
        /// Adds a new condition.
        /// </summary>
        /// <param name="condition">The condition to add.</param>
        void AddCondition(IBehaviour condition);

        /// <summary>
        /// Adds a new action.
        /// </summary>
        /// <param name="action">The action to add.</param>
        void AddAction(IBehaviour action);
    }
}