using UnityEngine;
using Status = Chinchillada.BehaviourSelections.BehaviourTree.Behaviour.Status;

namespace Chinchillada.BehaviourSelections.BehaviourTree.Tasks
{
    /// <summary>
    /// Task for moving away from a given target.
    /// </summary>
    internal class MoveAwayTask : MovementTask
    {
        /// <inheritdoc />
        protected override Status UpdateInternal()
        {
            //Check if we're far enough.
            float distance = Targeter.DistanceToTarget();
            if (distance > DistanceThreshold)
                return Status.Succes;

            //Move away.
            Vector2 directionToTarget = Targeter.DirectionToTarget();
            MovementController.ApplyMovement(-directionToTarget);

            return Status.Running;
        }
    }
}
