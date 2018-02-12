using System;
using UnityEngine;
using Chinchillada.BehaviourSelections.BehaviourTree.Builder;
using Status = Chinchillada.BehaviourSelections.BehaviourTree.Behaviour.Status;

namespace Chinchillada.BehaviourSelections.BehaviourTree.Tasks
{
    /// <summary>
    /// Base class for defining <see cref="BehaviourTree"/> behaviours as <see cref="MonoBehaviour"/>.
    /// </summary>
    internal abstract class Task : MonoBehaviour, IBehaviourBuilder
    {
        protected TaskBehaviour Behaviour { get; private set; }

        protected virtual void OnInitialization() { }
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
            private readonly Func<Status> _updateDelegate;
            private readonly System.Action _onInitialize;
            private readonly System.Action _onTerminate;

            public Status Status
            {
                get { return CurrentStatus; }
                set { CurrentStatus = value; }
            }

            public TaskBehaviour(Func<Status> updateDelegate, System.Action onInitialize, System.Action onTerminate, BehaviourTree tree) : base(tree)
            {
                _updateDelegate = updateDelegate;
                _onInitialize = onInitialize;
                _onTerminate = onTerminate;
            }

            protected override void Initialize()
            {
                _onInitialize.Invoke();
                base.Initialize();
            }

            public override void Terminate()
            {
                _onTerminate.Invoke();
                base.Terminate();
            }

            protected override Status UpdateInternal()
            {
                return _updateDelegate();
            }
        }
    }
}
