using UnityEngine;
using Status = Chinchillada.BehaviourSelections.BehaviourTree.Behaviour.Status;

namespace Chinchillada.BehaviourSelections.BehaviourTree.Tasks
{
    internal class MoveTowardsTask : MovementTask
    {
        protected override Behaviour.Status UpdateInternal()
        {
            //Lost target.
            if (Target == null)
                return Status.Failure;

            //Get positions.
            Vector3 currentPosition = transform.position;
            Vector3 targetPosition = Target.position;

            //Calculate difference.
            Vector3 difference = targetPosition - currentPosition;

            //Check if close enough.
            float distance = difference.magnitude;
            if (distance < DistanceThreshold)
                return Status.Succes;

            //Move towards target.
            Vector3 direction = difference.normalized;
            MovementController.ApplyMovement(direction);
            
            return Status.Running;
        }
    }
}
