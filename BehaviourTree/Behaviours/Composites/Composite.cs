using System.Collections.Generic;

namespace Chinchillada.BehaviourSelections.BehaviourTree
{
    /// <summary>
    /// Base class for behaviours that manipulate or manage multiple child behaviours.
    /// </summary>
    public abstract class Composite : Behaviour
    {
        /// <summary>
        /// The children behaviours this Composite manipulates.
        /// </summary>
        protected readonly List<IBehaviour> Children = new List<IBehaviour>();
        
        protected Composite(BehaviourTree tree) : base(tree) { }

        /// <summary>
        /// Composites don't have to update, so if it ever get's called by mistake, return running.
        /// </summary>
        /// <returns></returns>
        protected override Status UpdateInternal()
        {
            return Status.Running;
        }

        /// <summary>
        /// Suspends the composite.
        /// </summary>
        public void Suspend()
        {
            Tree.SuspendBehaviour(this);
            CurrentStatus = Status.Suspended;
        }

        /// <summary>
        /// Adds a child to the children of this composite.
        /// </summary>
        /// <param name="child"></param>
        public void AddChild(IBehaviour child) => Children.Add(child);

        /// <summary>
        /// Removes a child from the children of this composite.
        /// </summary>
        /// <param name="child"></param>
        public void RemoveChild(IBehaviour child) => Children.Remove(child);

        /// <summary>
        /// Clears all children from this composite.
        /// </summary>
        public void ClearChildren() => Children.Clear();
    }
}
