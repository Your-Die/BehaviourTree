using System;
using UnityEngine;
using Chinchillada.BehaviourSelections.BehaviourTree.Builder;
using Status = Chinchillada.BehaviourSelections.BehaviourTree.Behaviour.Status;

namespace Chinchillada.BehaviourSelections.BehaviourTree.Tasks
{
    /// <summary>
    /// Base class for defining <see cref="BehaviourTree"/> behaviours as <see cref="MonoBehaviour"/>.
    /// </summary>
    public abstract class Task : MonoBehaviour, IBehaviourBuilder
    {
        /// <summary>
        /// The behaviour.
        /// </summary>
        protected TaskBehaviour Behaviour { get; private set; }

        /// <summary>
        /// Called when the <see cref="Behaviour"/> initializes.
        /// </summary>
        protected virtual void OnInitialization() { }

        /// <summary>
        /// Called when the <see cref="Behaviour"/> terminates.
        /// </summary>
        protected virtual void OnTemination() { }

        /// <summary>
        /// Updates every frame while this <see cref="Task"/> is active.
        /// </summary>
        /// <returns>The status after this update.</returns>
        protected abstract Status UpdateInternal();

        /// <inheritdoc />
        public IBehaviour Build(BehaviourTree tree)
        {
            Behaviour = new TaskBehaviour(UpdateInternal, OnInitialization, OnTemination, tree);
            return Behaviour;
        }

        /// <summary>
        /// <see cref="Behaviour"/> that the <see cref="Task"/> wraps.
        /// </summary>
        protected class TaskBehaviour : Behaviour
        {
            /// <summary>
            /// Delegate for the update.
            /// </summary>
            private readonly Func<Status> _updateDelegate;

            /// <summary>
            /// Action called when this <see cref="Behaviour"/> initializes.
            /// </summary>
            private readonly System.Action _onInitialize;

            /// <summary>
            /// Action called when this <see cref="Behaviour"/> terminates.
            /// </summary>
            private readonly System.Action _onTerminate;

            /// <summary>
            /// The current status.
            /// </summary>
            public Status Taskstatus
            {
                get { return CurrentStatus; }
                set { CurrentStatus = value; }
            }

            /// <summary>
            /// Construct a new <see cref="TaskBehaviour"/>.
            /// </summary>
            /// <param name="updateDelegate">Delegate for the update</param>
            /// <param name="onInitialize"> Action called when this <see cref="Behaviour"/> initializes.</param>
            /// <param name="onTerminate">Action called when this <see cref="Behaviour"/> terminates</param>
            /// <param name="tree">The <see cref="BehaviourTree"/> this <see cref="Behaviour"/> is a part of.</param>
            public TaskBehaviour(Func<Status> updateDelegate, System.Action onInitialize, System.Action onTerminate, BehaviourTree tree) : base(tree)
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
            protected override Status UpdateInternal()
            {
                return _updateDelegate();
            }
        }
    }
}
