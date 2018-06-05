using System;

namespace Chinchillada.BehaviourSelections.BehaviourTree
{
    /// <summary>
    /// A behaviour in a behaviour tree.
    /// </summary>
    public abstract class Behaviour : IBehaviour
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
            /// The status when the behavior ended succesfully.
            /// </summary>
            Succes,

            /// <summary>
            /// The status when the behavior terminated in failure.
            /// </summary>
            Failure,

            /// <summary>
            /// The status when the behavior is suspended temporarily.
            /// </summary>
            Suspended
        }

        /// <summary>
        /// Event called when the behaviour terminates.
        /// </summary>
        public event Action<IBehaviour, Status> Terminated;

        /// <summary>
        /// The current status.
        /// </summary>
        public Status CurrentStatus { get; protected set; } = Status.Invalid;

        /// <summary>
        /// If the behaviour is terminated or not.
        /// </summary>
        public bool IsTerminated { get; private set; } = true;

        /// <summary>
        /// The <see cref="BehaviourTree"/> this <see cref="IBehaviour"/> is a part of.
        /// </summary>
        public BehaviourTree Tree { get; set; }

        /// <summary>
        /// Construct a new <see cref="Behaviour"/>.
        /// </summary>
        /// <param name="tree">The tree this <see cref="IBehaviour"/> is a part of.</param>
        protected Behaviour(BehaviourTree tree)
        {
            Tree = tree;
        }

        /// <summary>
        /// Updates the behavour.
        /// </summary>
        /// <returns>The status after the update.</returns>
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
        /// The actual updating of the behavior is delegated to subclasses.
        /// </summary>
        /// <returns></returns>
        protected virtual Status UpdateInternal()
        {
            return Status.Running;
        }

        /// <summary>
        /// Terminate the behavior.
        /// </summary>
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

        public void StartBehaviour()
        {
            if (CurrentStatus != Status.Running && CurrentStatus != Status.Suspended)
                Tree.StartBehaviour(this);
        }
        
        /// <summary>
        /// Suspends the <see cref="IBehaviour"/>.
        /// </summary>
        public void Suspend()
        {
            Tree.SuspendBehaviour(this);
            CurrentStatus = Status.Suspended;
        }

        public void Continue()
        {
            if (CurrentStatus == Status.Suspended)
                Tree.ContinueBehaviour(this);
        }
    }
}
