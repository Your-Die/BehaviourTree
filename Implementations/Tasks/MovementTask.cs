using Chinchillada.Movement;
using UnityEngine;

namespace Chinchillada.BehaviourSelections.BehaviourTree.Tasks
{
    internal abstract class MovementTask : Task
    {
        [SerializeField] private Transform _target;

        [SerializeField] private float _distanceThreshold;

        private MovementController _movementController;
        private ITargeter _targeter;

        protected float DistanceThreshold => _distanceThreshold;

        protected ITargeter Targeter => _targeter;

        protected MovementController MovementController => _movementController;

        public Transform Target
        {
            get { return _target; }
            set { _target = value; }
        }

        protected virtual void Awake()
        {
            _movementController = GetComponentInParent<MovementController>();
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
