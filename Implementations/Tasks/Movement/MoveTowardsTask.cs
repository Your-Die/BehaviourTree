using UnityEngine;

namespace Chinchillada.BehaviourSelections.BehaviorTree.Tasks
{
    /// <summary>
    /// Task that moves towards a given target.
    /// </summary>
    internal class MoveTowardsTask : MovementTask
    {
        /// <summary>
        /// The distance that is close enough to count as a <see cref="Behavior.Status.Success"/>.
        /// </summary>
        [SerializeField] private float _targetReachedDistance = 0.1f;

        /// <inheritdoc />
        protected override Behavior.Status UpdateInternal()
        {
            //Lost target.
            if (!Targeter.HasTarget)
                return BehaviorTree.Behavior.Status.Failure;
             
            //Check if close enough. 
            float distance = Targeter.DistanceToTarget(); 
            if (distance < _targetReachedDistance)
                return BehaviorTree.Behavior.Status.Success;

            //Move towards target. 
            Vector3 direction = Targeter.DirectionToTarget();
            MovementController.ApplyMovement(direction);
            
            return BehaviorTree.Behavior.Status.Running;
        }
    }
}
