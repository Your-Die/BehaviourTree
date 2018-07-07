using System;

namespace Chinchillada.BehaviourSelections.BehaviourTree
{
    /// <summary>
    /// base class for behaviours that add additional functionality to the child behaviour it decorates.
    /// </summary>
    public abstract class Decorator : Behaviour
    {
        /// <summary>
        /// The child this behaviour decorates.
        /// </summary>
        internal IBehaviour Child { get; set; }

        /// <summary>
        /// Construct a new Decorator Behaviour.
        /// </summary>
        /// <param name="tree">The behaviour tree this node is a part of.</param>
        /// <param name="child"><see cref="Child"/>.</param>
        protected Decorator(BehaviourTree tree, IBehaviour child) : base(tree)
        {
            Child = child;
        }
    }
}
