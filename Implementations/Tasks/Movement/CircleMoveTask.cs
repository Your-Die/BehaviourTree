using UnityEngine;
using Chinchillada.BehaviourSelections.Utilities;

namespace Chinchillada.BehaviourSelections.BehaviorTree.Tasks
{ 
    /// <summary>
    /// <see cref="MovementTask"/> that moves in a circle around the target.
    /// </summary>
    public class CircleMoveTask : MovementTask
    {
        /// <summary>
        /// Whether to move clockwise or counter-clockwise.
        /// </summary>
        public bool Clockwise;
        
        /// <inheritdoc />
        protected override Behavior.Status UpdateInternal()
        {
            //Ensure target.
            if (!Targeter.HasTarget)
                return BehaviorTree.Behavior.Status.Failure;

            //Get the direction.
            Vector2 directionToTarget = Targeter.DirectionToTarget();

            //Calculate the perpendicular vector to the direction.
            Vector2 perpendicular = Clockwise
                ? directionToTarget.PerpendicularCW()
                : directionToTarget.PerpendicularCCW();
            
            //Move.
            MovementController.ApplyMovement(perpendicular);
            return BehaviorTree.Behavior.Status.Running;
        }
    }
}
