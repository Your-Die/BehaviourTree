using UnityEngine;
using Assets.BehaviourSelections.BahaviorTree.Builder;
using Chinchillada.BehaviourSelections.BehaviourTree.Tasks;

internal class DistanceCondition : ConditionBuilder
{
    [SerializeField] private float _minDistance = 0;
    [SerializeField] private float _maxDistance = 5;

    private ITargeter _targeter;

    private void Awake()
    {
        _targeter = GetComponentInParent<ITargeter>();
    }

    protected override bool ValidateCondition()
    {
        Transform target = _targeter.GetTarget();
        float distance = Vector3.Distance(transform.position, target.position);

        return distance >= _minDistance  && distance <= _maxDistance;
    }
}
