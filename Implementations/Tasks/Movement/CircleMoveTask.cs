using UnityEngine;
using Chinchillada.BehaviourSelections.Utilities;
using Status = Chinchillada.BehaviourSelections.BehaviourTree.Behaviour.Status;

namespace Chinchillada.BehaviourSelections.BehaviourTree.Tasks
{ 
    /// <summary>
    /// <see cref="MovementTask"/> that moves in a circle around the target.
    /// </summary>
    public class CircleMoveTask : MovementTask
    {
        /// <summary>
        /// Wether to move clockwise or counter-clockwise.
        /// </summary>
        public bool Clockwise;
        
        /// <inheritdoc />
        protected override Status UpdateInternal()
        {
            //Ensure target.
            if (!Targeter.HasTarget)
                return Status.Failure;

            //Get the direciton.
            Vector2 directionToTarget = Targeter.DirectionToTarget();

            //Calculate the perpendicular vector to the direction.
            Vector2 perpendicular = Clockwise
                ? directionToTarget.PerpendicularCW()
                : directionToTarget.PerpendicularCCW();
            
            //Move.
            MovementController.ApplyMovement(perpendicular);
            return Status.Running;
        }
    }
}
