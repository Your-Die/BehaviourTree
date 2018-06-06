using UnityEngine;
using Status = Chinchillada.BehaviourSelections.BehaviourTree.Behaviour.Status;

namespace Chinchillada.BehaviourSelections.BehaviourTree.Tasks
{ 
    public class CircleMoveTask : MovementTask
    {
        private float _angle;

        protected override void OnInitialization()
        {
            base.OnInitialization();
            
            _angle = Vector2.Angle(Target.position, transform.position);
        }

        protected override Status UpdateInternal()
        {
            //Ensure target.
            if (!Targeter.HasTarget())
                return Status.Failure;

            //Update angle.
            _angle += MovementController.SpeedPerUpdate;

            //Calculate offset.
            float sin = Mathf.Sin(_angle);
            float cos = Mathf.Cos(_angle);
            Vector3 offset = new Vector2(sin, cos);

            //Calculate new position.
            offset *= Targeter.DistanceToTarget();
            Vector2 position = Target.position + offset;

            //Move towards new position.
            MovementController.MoveTowards(position);
            return Status.Running;
        }
    }
}
