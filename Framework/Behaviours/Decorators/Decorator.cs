using System;

namespace Chinchillada.BehaviourSelections.BehaviourTree
{
    /// <summary>
    /// base class for behaviours that decorate the <see cref="Child"/> <see cref="IBehaviour"/> with additional behaviour.
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


        protected abstract void OnChildTerminated(IBehaviour child, Status status);
    }
}
