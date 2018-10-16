using Chinchillada.Movement;
using UnityEngine;

namespace Chinchillada.BehaviourSelections.BehaviorTree.Tasks
{
    /// <summary>
    /// Base class for <see cref="Task"/>s that handle moving.
    /// </summary>
    public abstract class MovementTask : Task
    {
        /// <summary>
        /// Keeps track of the target.
        /// </summary>
        protected ITargeter Targeter { get; private set; }

        /// <summary>
        /// Handles the actual movement.
        /// </summary>
        protected MovementController MovementController { get; private set; }

        /// <summary>
        /// The target to which we move relatively.
        /// </summary>
        [SerializeField] public Transform Target { get; set; }

        /// <summary>
        /// Get necessary components.
        /// </summary>
        protected virtual void Awake()
        {
            MovementController = GetComponentInParent<MovementController>();
            Targeter = GetComponentInParent<ITargeter>();
        }
        
        /// <summary>
        /// Set the active <paramref name="target"/>.
        /// </summary> 
        private void SetTarget(Transform target)
        {
            Target = target;
        }

        /// <inheritdoc />
        protected override void OnInitialization()
        {
            //We meed a targeter.
            if (Targeter == null)
                return;

            //Get the target.
            Target = Targeter.GetTarget();
            Targeter.TargetChanged += SetTarget;
        }

        /// <inheritdoc />
        protected override void OnTermination()
        {
            //Stop moving.
            MovementController.StopMovement();
            Targeter.TargetChanged -= SetTarget;
        }

        /// <summary>
        /// Called when this object is destroyed.
        ///  </summary>
        protected virtual void OnDestroy()
        {
            //Ensure no lingering events are triggered.
            Targeter.TargetChanged -= SetTarget;
        }
    }
}
