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
        /// <summary>
        /// Updates every frame while this <see cref="Task"/> is active.
        /// </summary>
        /// <returns>The status after this update.</returns>
        protected abstract Status UpdateInternal();

        /// <inheritdoc />
        public IBehaviour Build(BehaviourTree tree)
        {
            return new TaskBehaviour(UpdateInternal, tree);
        }

        /// <summary>
        /// <see cref="Behaviour"/> that the <see cref="Task"/> wraps.
        /// </summary>
        private class TaskBehaviour : Behaviour
        {
            private readonly Func<Status> _updateDelegate;

            public TaskBehaviour(Func<Status> updateDelegate, BehaviourTree tree) : base(tree)
            {
                _updateDelegate = updateDelegate;
            }

            protected override Status UpdateInternal()
            {
                return _updateDelegate();
            }
        }
    }
}
