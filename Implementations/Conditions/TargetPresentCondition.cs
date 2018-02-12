using Assets.BehaviourSelections.BahaviorTree.Builder;
using Chinchillada.BehaviourSelections.BehaviourTree.Tasks;
using UnityEngine;

namespace Chinchillada.BehaviourSelections.BehaviourTree
{
    internal class TargetPresentCondition : ConditionBuilder
    {
        [SerializeField] private ITargeter _targeter;

        private void Awake()
        {
            _targeter = GetComponentInParent<ITargeter>();
        }

        protected override bool ValidateCondition()
        {
            return _targeter.GetTarget() != null;
        }
    }
}
