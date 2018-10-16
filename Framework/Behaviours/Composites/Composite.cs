using System.Collections.Generic;

namespace Chinchillada.BehaviourSelections.BehaviorTree
{
    /// <summary>
    /// Base class for behaviours that manipulate or manage multiple child behaviours.
    /// </summary>
    public abstract class Composite : Behavior
    {
        /// <summary>
        /// The children behaviours this Composite manipulates.
        /// </summary>
        protected readonly List<IBehavior> Children = new List<IBehavior>();
        
        protected Composite(BehaviourTree tree) : base(tree) { }

        /// <summary>
        /// Composites don't have to update, so if it ever get's called by mistake, return running.
        /// </summary>
        /// <returns></returns>
        protected override Status UpdateInternal()
        {
            return Status.Suspended;
        }

        /// <summary>
        /// Adds a child to the children of this composite.
        /// </summary>
        /// <param name="child"></param>
        public void AddChild(IBehavior child) => Children.Add(child);

        /// <summary>
        /// Removes a child from the children of this composite.
        /// </summary>
        /// <param name="child"></param>
        public void RemoveChild(IBehavior child) => Children.Remove(child);

        /// <summary>
        /// Clears all children from this composite.
        /// </summary>
        public void ClearChildren() => Children.Clear();
    }
}
