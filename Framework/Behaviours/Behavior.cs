using System;

namespace Chinchillada.BehaviourSelections.BehaviorTree
{
    /// <summary>
    /// A behaviour in a behaviour tree.
    /// </summary>
    public abstract class Behavior : IBehavior
    {
        /// <summary>
        /// The status.
        /// </summary>
        public enum Status
        {
            /// <summary>
            /// Not a valid active state. This is the status when the behavior is inactive.
            /// </summary>
            Invalid,

            /// <summary>
            /// The status when running.
            /// </summary>
            Running,

            /// <summary>
            /// The status when the behavior ended successfully.
            /// </summary>
            Success,

            /// <summary>
            /// The status when the behavior terminated in failure.
            /// </summary>
            Failure,

            /// <summary>
            /// The status when the behavior is suspended temporarily.
            /// </summary>
            Suspended
        }

        /// <inheritdoc />
        public event Action<IBehavior, Status> Terminated;

        /// <inheritdoc />
        public BehaviourTree Tree { get; set; }

        /// <inheritdoc />
        public Status CurrentStatus { get; protected set; } = Status.Invalid;

        /// <inheritdoc />
        public bool IsTerminated { get; private set; } = true;
        
        /// <summary>
        /// Construct a new <see cref="Behavior"/>.
        /// </summary>
        /// <param name="tree">The tree this <see cref="IBehavior"/> is a part of.</param>
        protected Behavior(BehaviourTree tree)
        {
            Tree = tree;
        }

        /// <inheritdoc />
        public Status Tick()
        {
            //Initialize if we're not running.
            if (IsTerminated)
                Initialize();

            if (CurrentStatus == Status.Suspended)
                return Status.Suspended;

            //Update.
            CurrentStatus = UpdateInternal();

            //Terminate if we're not running anymore.
            if (CurrentStatus != Status.Running)
                Terminate();

            return CurrentStatus;
        }

        /// <summary>
        /// Initializes the behavior. Called when <see cref="Tick"/> is called on an inactive behavior.
        /// </summary>
        protected virtual void Initialize()
        {
            IsTerminated = false;
        }

        /// <summary>
        /// Virtual sandbox method where implementing child classes can implement their behaviour.
        /// Called every frame that the behaviour is active.
        /// </summary>
        /// <returns>Status at the end of the update tick.</returns>
        protected virtual Status UpdateInternal()
        {
            return CurrentStatus;
        }

        /// <inheritdoc />
        public virtual void Terminate()
        {
            IsTerminated = true;
            Terminated?.Invoke(this, CurrentStatus);
        }

        /// <inheritdoc />
        public void Terminate(Status status)
        {
            CurrentStatus = status;
            Terminate();
        }

        /// <inheritdoc />
        public void StartBehaviour()
        {
            if (IsTerminated)
                Tree.StartBehaviour(this);
        }

        /// <inheritdoc />
        public void StartNextFrame()
        {
            Tree.StartNextFrame(this);
        }

        /// <inheritdoc />
        public void Suspend()
        {
            Tree.SuspendBehaviour(this);
            CurrentStatus = Status.Suspended;
        }

        /// <inheritdoc />
        public void Continue()
        {
            if (CurrentStatus == Status.Suspended)
                Tree.ContinueBehaviour(this);
        }
    }
}
