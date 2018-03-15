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
             
            //Check if close enough. 
            float distance = Targeter.DistanceToTarget();
            if (distance < DistanceThreshold)
                return Status.Succes;

            //Move towards target. 
            Vector3 direction = Targeter.DirectionToTarget();
            MovementController.ApplyMovement(direction);
            
            return Status.Running;
        }
    }
}
