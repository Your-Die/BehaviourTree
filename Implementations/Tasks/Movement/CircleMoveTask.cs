using UnityEngine;
using Chinchillada.BehaviourSelections.Utilities;
using Status = Chinchillada.BehaviourSelections.BehaviourTree.Behaviour.Status;

namespace Chinchillada.BehaviourSelections.BehaviourTree.Tasks
{ 
    public class CircleMoveTask : MovementTask
    {
        public bool Clockwise;
      
        protected override Status UpdateInternal()
        {
            //Ensure target.
            if (!Targeter.HasTarget)
                return Status.Failure;

            Vector2 directionToTarget = Targeter.DirectionToTarget();
            Vector2 perpendicular = Clockwise
                ? directionToTarget.PerpendicularCW()
                : directionToTarget.PerpendicularCCW();
            
            MovementController.ApplyMovement(perpendicular);
            return Status.Running;
        }
    }
}
