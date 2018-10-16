using System;

namespace Chinchillada.BehaviourSelections.BehaviorTree
{
    /// <summary>
    /// base class for behaviours that decorate the <see cref="Child"/> <see cref="IBehavior"/> with additional behaviour.
    /// </summary>
    public abstract class Decorator : Behavior
    {
        /// <summary>
        /// The child this behaviour decorates.
        /// </summary>
        internal IBehavior Child { get; set; }

        /// <summary>
        /// Construct a new Decorator Behavior.
        /// </summary>
        /// <param name="tree">The behaviour tree this node is a part of.</param>
        /// <param name="child"><see cref="Child"/>.</param>
        protected Decorator(BehaviourTree tree, IBehavior child) : base(tree)
        {
            Child = child;
        }

        protected override void Initialize()
        {
            base.Initialize();

            Child.Terminated += OnChildTerminated;
        }


        public override void Terminate()
        {
            Child.Terminated -= OnChildTerminated;

            //Ensure child is also terminated.
            if (!Child.IsTerminated)
                Child.Terminate(CurrentStatus);

            base.Terminate();
        }


        protected abstract void OnChildTerminated(IBehavior child, Status status);
    }
}
