using UnityEngine;
using Status = Chinchillada.BehaviourSelections.BehaviourTree.Behaviour.Status;

namespace Chinchillada.BehaviourSelections.BehaviourTree.Tasks
{
    /// <summary>
    /// A <see cref="Task"/> for idling for a given duration.
    /// </summary>
    internal class IdleTask : Task
    {
        /// <summary>
        /// The duration to idle.
        /// </summary>
        [SerializeField] private float _duration; 

        /// <inheritdoc />
        protected override void OnInitialization()
        {
            //Wait for the duration.
            Behaviour.Suspend();
            Invoke(nameof(Finish), _duration);
        }

        /// <inheritdoc />
        protected override Status UpdateInternal()
        {
            return Behaviour.Taskstatus;
        }

        /// <summary>
        /// Called when the duration has finished.
        /// Terminates this behaviour.
        /// </summary>
        private void Finish()
        {
            Behaviour.Terminate(Status.Success);
        }
    }
}
