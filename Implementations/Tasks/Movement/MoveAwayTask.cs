using UnityEngine;
using Status = Chinchillada.BehaviourSelections.BehaviourTree.Behaviour.Status;

namespace Chinchillada.BehaviourSelections.BehaviourTree.Tasks
{
    /// <summary>
    /// <see cref="MovementTask"/> that moves away from the target.
    /// </summary>
    internal class MoveAwayTask : MovementTask
    {
        /// <summary>
        /// The distance that is seen as far enough.
        /// </summary>
        [SerializeField] private float _farEnoughDistance = 3;

        /// <inheritdoc />
        protected override Status UpdateInternal()
        { 
            //Check if we're far enough.
            float distance = Targeter.DistanceToTarget();
            if (distance > _farEnoughDistance)
                return Status.Success;

            //Move away.
            Vector2 directionToTarget = Targeter.DirectionToTarget();
            MovementController.ApplyMovement(-directionToTarget);

            return Status.Running;
        }
    }
}
