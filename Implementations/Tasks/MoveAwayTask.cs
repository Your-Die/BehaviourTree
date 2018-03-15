using UnityEngine;
using Status = Chinchillada.BehaviourSelections.BehaviourTree.Behaviour.Status;

namespace Chinchillada.BehaviourSelections.BehaviourTree.Tasks
{
    internal class MoveAwayTask : MovementTask
    {
        protected override Status UpdateInternal()
        {
            float distance = Targeter.DistanceToTarget();
            if (distance > DistanceThreshold)
                return Status.Succes;

            Vector2 directionToTarget = Targeter.DirectionToTarget();
            MovementController.ApplyMovement(-directionToTarget);

            return Status.Running;
        }
    }
}
