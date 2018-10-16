using Chinchillada.BehaviourSelections.BehaviorTree.Builder;
using System;
using UnityEngine;

namespace Chinchillada.BehaviourSelections.BehaviorTree.Tasks
{
    /// <summary>
    /// Base class for defining <see cref="BehaviourTree"/> behaviors as <see cref="MonoBehaviour"/>.
    /// </summary>
    public abstract class Task : BehaviourBuilder
    {
        ///<inheritdoc />
        protected override string TypeName => "Task";

        /// <summary>
        /// The behaviour.
        /// </summary>
        protected TaskBehavior Behavior { get; private set; }

        /// <summary>
        /// Called when the <see cref="Behavior"/> initializes.
        /// </summary>
        protected virtual void OnInitialization() { }

        /// <summary>
        /// Called when the <see cref="Behavior"/> terminates.
        /// </summary>
        protected virtual void OnTermination() { }

        /// <summary>
        /// Updates every frame while this <see cref="Task"/> is active.
        /// </summary>
        /// <returns>The status after this update.</returns>
        protected abstract Behavior.Status UpdateInternal();

        /// <inheritdoc />
        public override IBehavior Build(BehaviourTree tree)
        {
            Behavior = new TaskBehavior(UpdateInternal, OnInitialization, OnTermination, tree);
            return Behavior;
        }

        /// <summary>
        /// <see cref="Behavior"/> that the <see cref="Task"/> wraps.
        /// </summary>
        protected class TaskBehavior : Behavior
        {
            /// <summary>
            /// Delegate for the update.
            /// </summary>
            private readonly Func<Status> _updateDelegate;

            /// <summary>
            /// Action called when this <see cref="Behavior"/> initializes.
            /// </summary>
            private readonly System.Action _onInitialize;

            /// <summary>
            /// Action called when this <see cref="Behavior"/> terminates.
            /// </summary>
            private readonly System.Action _onTerminate;

            /// <summary>
            /// The current status.
            /// </summary>
            public Status TaskStatus
            {
                get { return CurrentStatus; }
                set { CurrentStatus = value; }
            }

            /// <summary>
            /// Construct a new <see cref="TaskBehavior"/>.
            /// </summary>
            /// <param name="updateDelegate">Delegate for the update</param>
            /// <param name="onInitialize"> Action called when this <see cref="Behavior"/> initializes.</param>
            /// <param name="onTerminate">Action called when this <see cref="Behavior"/> terminates</param>
            /// <param name="tree">The <see cref="BehaviourTree"/> this <see cref="Behavior"/> is a part of.</param>
            public TaskBehavior(Func<Status> updateDelegate, System.Action onInitialize, System.Action onTerminate, BehaviourTree tree) : base(tree)
            {
                //Cache the delegates.
                _updateDelegate = updateDelegate;
                _onInitialize = onInitialize;
                _onTerminate = onTerminate;
            }

            ///<inheritdoc />
            protected override void Initialize()
            {
                _onInitialize.Invoke();
                base.Initialize();
            }

            ///<inheritdoc />
            public override void Terminate()
            {
                _onTerminate.Invoke();
                base.Terminate();
            }

            ///<inheritdoc />
            protected override Status UpdateInternal() => _updateDelegate();
        }
    }
}
