using System;
using Chinchillada.BehaviourSelections.BehaviourTree.Tasks;
using UnityEngine;

/// <summary>
/// Targeter that targets a <see cref="Transform"/> set through the inspector.
/// </summary>
public class Targeter : MonoBehaviour, ITargeter
{
    /// <summary>
    /// The target.
    /// </summary>
    [SerializeField] private Transform _target;

    /// <summary>
    /// Cache of the target used to notice a changed target.
    /// </summary>
    private Transform _targetCache;
    
    /// <summary>
    /// The target.
    /// </summary>
    public Transform Target
    {
        get { return _target; }
        set
        {
            _target = value;
            CheckTargetChanged();
        }
    }

    /// <summary>
    /// Event invoked when the <see cref="Target"/> is changed.
    /// </summary>
    public event Action<Transform> TargetChanged;

    /// <summary>
    /// Called when this <see cref="MonoBehaviour"/> awakens.
    /// </summary>
    private void Awake()
    {
        //Cache the target.
        _targetCache = _target;
    }

    /// <summary>
    /// Called when the inspector values on this <see cref="MonoBehaviour"/> have been altered.
    /// </summary>
    private void OnValidate()
    {
        CheckTargetChanged();
    }

    /// <summary>
    /// Checks if the target is changed and invokes <see cref="TargetChanged"/>.
    /// </summary>
    private void CheckTargetChanged()
    { 
        //Still the same.
        if (_target == _targetCache)
            return;

        //Update cache, invoke event.
        _targetCache = Target;
        TargetChanged?.Invoke(_target);
    }

    /// <inheritdoc />
    public Transform GetTarget()
    {
        return _target;
    }

    /// <inheritdoc />
    public bool HasTarget => _target != null;

    /// <inheritdoc />
    public float DistanceToTarget()
    {
        return Vector2.Distance(transform.position, _target.position);
    }

    /// <inheritdoc />
    public Vector3 DirectionToTarget()
    {
        Vector3 difference = _target.position - transform.position;
            return difference.normalized;
    }
}
