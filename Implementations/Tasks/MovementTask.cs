using Chinchillada.Movement;
using UnityEngine;

namespace Chinchillada.BehaviourSelections.BehaviourTree.Tasks
{
    internal abstract class MovementTask : Task
    {
        [SerializeField] private Transform _target;

        [SerializeField] private float _distanceThreshold;

        [SerializeField] private bool _findMovementOnAwake;
        [SerializeField] private bool _findTargeterOnAwake;

        [SerializeField] private MovementController _movementController;
        [SerializeField] private ITargeter _targeter;

        protected float DistanceThreshold => _distanceThreshold;

        protected MovementController MovementController => _movementController;

        public Transform Target
        {
            get { return _target; }
            set { _target = value; }
        }

        protected virtual void Awake()
        {
            if (_findMovementOnAwake)
                _movementController = GetComponentInParent<MovementController>();

            if (_findTargeterOnAwake)
                _targeter = GetComponentInParent<ITargeter>(); 
        }

        private void SetTarget(Transform target)
        {
            Target = target;
        }

        protected override void OnInitialization()
        {
            if (_targeter == null)
                return;

            _target = _targeter.GetTarget();
            _targeter.TargetChanged += SetTarget;
        }

        protected override void OnTemination()
        {
            MovementController.StopMovement();
            _targeter.TargetChanged -= SetTarget;
        }

        protected virtual void OnDestroy()
        {
            _targeter.TargetChanged -= SetTarget;
        }
    }
}
