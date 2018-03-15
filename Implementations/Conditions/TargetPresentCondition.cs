using Assets.BehaviourSelections.BahaviorTree.Builder;
using Chinchillada.BehaviourSelections.BehaviourTree.Tasks;
using UnityEngine;

namespace Chinchillada.BehaviourSelections.BehaviourTree
{
    /// <summary>
    /// Condition that checks if a target is present.
    /// </summary>
    internal class TargetPresentCondition : ConditionBuilder
    {
        /// <summary>
        /// Targeter that keeps track of all valid targets.
        /// </summary>
        [SerializeField] private ITargeter _targeter;

        /// <summary>
        /// Get reference to the <see cref="_targeter"/>.
        /// </summary>
        private void Awake()
        {
            _targeter = GetComponentInParent<ITargeter>();
        }

        ///<inheritdoc />
        protected override bool ValidateCondition()
        {
            //Check if a target is present.
            return _targeter.GetTarget() != null;
        }
    }
}
