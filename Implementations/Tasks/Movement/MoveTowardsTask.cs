using UnityEngine;
using Status = Chinchillada.BehaviourSelections.BehaviourTree.Behaviour.Status;

namespace Chinchillada.BehaviourSelections.BehaviourTree.Tasks
{
    /// <summary>
    /// Task that moves towards a given target.
    /// </summary>
    internal class MoveTowardsTask : MovementTask
    {
        [SerializeField] private float _targetReachedDistance = 0.1f;

        /// <inheritdoc />
        protected override Status UpdateInternal()
        {
            //Lost target.
            if (!Targeter.HasTarget())
                return Status.Failure;
             
            //Check if close enough. 
            float distance = Targeter.DistanceToTarget();
             
            if (distance < _targetReachedDistance)
                return Status.Succes;

            //Move towards target. 
            Vector3 direction = Targeter.DirectionToTarget();
            MovementController.ApplyMovement(direction);
            
            return Status.Running;
        }
    }
}
