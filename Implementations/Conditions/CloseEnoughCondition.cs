using Assets.BehaviourSelections.BahaviorTree.Builder;
using Chinchillada.BehaviourSelections.BehaviourTree.Tasks;
using UnityEngine;

namespace Chinchillada.BehaviourSelections.BehaviourTree
{
    internal class CloseEnoughCondition : ConditionBuilder
    {
        [SerializeField] private float _distance;

        private ITargeter _targeter;

        private void Awake()
        {
            _targeter = GetComponentInParent<ITargeter>();
        }

        protected override bool ValidateCondition()
        {
            Transform target = _targeter.GetTarget();
            float distance = Vector3.Distance(transform.position, target.position);

            return distance <= _distance;
        }
    }
}
