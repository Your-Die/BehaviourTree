namespace Chinchillada.BehaviourSelections.BehaviorTree.Builder
{
    /// <summary>
    /// Interface for Unity components that are used to build behaviors for building a behaviour tree.
    /// </summary>
    internal interface IBehaviourBuilder
    {
        /// <summary>
        /// Builds the behaviour.
        /// </summary>
        /// <returns>The behaviour.</returns>
        IBehavior Build(BehaviourTree tree);
    }
}
