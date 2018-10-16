using UnityEngine;
using Status = Chinchillada.BehaviourSelections.BehaviorTree.Behavior.Status;

namespace Chinchillada.BehaviourSelections.BehaviorTree.Tasks
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
            Behavior.Suspend();
            Invoke(nameof(Finish), _duration);
        }

        /// <inheritdoc />
        protected override Behavior.Status UpdateInternal()
        {
            return Behavior.TaskStatus;
        }

        /// <summary>
        /// Called when the duration has finished.
        /// Terminates this behaviour.
        /// </summary>
        private void Finish()
        {
            Behavior.Terminate(BehaviorTree.Behavior.Status.Success);
        }
    }
}
