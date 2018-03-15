using UnityEngine;
using Assets.BehaviourSelections.BahaviorTree.Builder;
using Chinchillada.BehaviourSelections.BehaviourTree.Tasks;

/// <summary>
/// Condition that checks if the game object is in a given range of it's target.
/// </summary>
internal class DistanceCondition : ConditionBuilder
{
    /// <summary>
    /// The lower bound of the range of allowed distances.
    /// </summary>
    [SerializeField] private float _minDistance = 0;

    /// <summary>
    /// The upper bound of the range of allowed distances.
    /// </summary>
    [SerializeField] private float _maxDistance = 5;

    /// <summary>
    /// The targeter that keeps track of our target.
    /// </summary>
    private ITargeter _targeter;

    /// <summary>
    /// Get the reference to the <see cref="_targeter"/>.
    /// </summary>
    private void Awake()
    {
        _targeter = GetComponentInParent<ITargeter>();
    }

    /// <inheritdoc />
    protected override bool ValidateCondition()
    {
        //Get the distance to the targeter.
        Transform target = _targeter.GetTarget();
        float distance = Vector3.Distance(transform.position, target.position);

        //Check if the distance is in range.
        return distance >= _minDistance  && distance <= _maxDistance;
    }
}
