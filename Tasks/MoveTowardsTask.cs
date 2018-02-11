using UnityEngine;

namespace Chinchillada.BehaviourSelections.BehaviourTree.Tasks
{
    internal class MoveTowardsTask : MovementTask
    {
        protected override Behaviour.Status UpdateInternal()
        {
            //Lost target.
            if (Target == null)
                return Behaviour.Status.Failure;

            //Get positions.
            Vector3 currentPosition = transform.position;
            Vector3 targetPosition = Target.position;

            //Calculate difference.
            Vector3 difference = targetPosition - currentPosition;

            //Check if close enough.
            float distance = difference.magnitude;
            if (distance < DistanceThreshold)
                return Behaviour.Status.Succes;

            //Move towards target.
            Vector3 direction = difference.normalized;
            MovementController.ApplyMovement(direction);
            
            return Behaviour.Status.Running;
        }
    }
}
