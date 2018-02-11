namespace Chinchillada.BehaviourSelections.BehaviourTree.Builder
{
    /// <summary>
    /// Interface for Unity components that are used to build behaviours for building a behaviour tree.
    /// </summary>
    internal interface IBehaviourBuilder
    {
        /// <summary>
        /// Builds the behaviour.
        /// </summary>
        /// <returns>The behaviour.</returns>
        IBehaviour Build(BehaviourTree tree);
    }
}
