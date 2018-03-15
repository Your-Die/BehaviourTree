using Chinchillada.Movement;
using UnityEngine;

namespace Chinchillada.BehaviourSelections.BehaviourTree.Tasks
{
    /// <summary>
    /// Base class for tasks that handle moving.
    /// </summary>
    internal abstract class MovementTask : Task
    {
        /// <summary>
        /// The target to which we move relatively.
        /// </summary>
        [SerializeField] private Transform _target;

        /// <summary>
        /// The threshhold of distance to the <see cref="_target"/>.
        /// </summary>
        [SerializeField] private float _distanceThreshold;

        /// <summary>
        /// Handles the actual movement.
        /// </summary>
        private MovementController _movementController;

        /// <summary>
        /// Keeps track of the target.
        /// </summary>
        private ITargeter _targeter;

        /// <summary>
        /// The threshhold of distance to the <see cref="_target"/>.
        /// </summary>
        protected float DistanceThreshold => _distanceThreshold;

        /// <summary>
        /// Keeps track of the target.
        /// </summary>
        protected ITargeter Targeter => _targeter;

        /// <summary>
        /// Handles the actual movement.
        /// </summary>
        protected MovementController MovementController => _movementController;

        /// <summary>
        /// The target to which we move relatively.
        /// </summary>
        public Transform Target
        {
            get { return _target; }
            set { _target = value; }
        }

        /// <summary>
        /// Get necessary components.
        /// </summary>
        protected virtual void Awake()
        {
            _movementController = GetComponentInParent<MovementController>();
            _targeter = GetComponentInParent<ITargeter>();
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
            if (_targeter == null)
                return;

            //Get the target.
            _target = _targeter.GetTarget();
            _targeter.TargetChanged += SetTarget;
        }

        /// <inheritdoc />
        protected override void OnTemination()
        {
            //Stop moving.
            MovementController.StopMovement();
            _targeter.TargetChanged -= SetTarget;
        }

        protected virtual void OnDestroy()
        {
            //Ensure no lingering events are triggered.
            _targeter.TargetChanged -= SetTarget;
        }
    }
}
