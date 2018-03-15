using UnityEngine;

namespace Chinchillada.BehaviourSelections.BehaviourTree.Builder
{
    /// <summary>
    /// Base class for <see cref="IBehaviourBuilder"/> implementations that build <see cref="Action"/> behaviours.
    /// </summary>
    internal abstract class ActionBuilder : MonoBehaviour, IBehaviourBuilder
    {
        /// <summary>
        /// Does the action.
        /// </summary>
        /// <returns>True if the action was succesfull.</returns>
        protected abstract bool DoAction();

        /// <summary>
        /// Builds the <see cref="Action"/>.
        /// </summary>
        /// <param name="tree">The tree the resulting <see cref="Action"/> is to be a part of.</param>
        /// <returns>The build <see cref="Action"/>.</returns>
        public IBehaviour Build(BehaviourTree tree)
        {
            enabled = false;
            return new Action(tree, DoAction);
        }
    }
}
