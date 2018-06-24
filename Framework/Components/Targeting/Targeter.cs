using System;
using Chinchillada.BehaviourSelections.BehaviourTree.Tasks;
using UnityEngine;

public class Targeter : MonoBehaviour, ITargeter
{
    [SerializeField] private Transform _target;

    private Transform _targetCache;

    public Transform Target
    {
        get { return _target; }
        set
        {
            _target = value;
            CheckTargetChanged();
        }
    }

    public event Action<Transform> TargetChanged;

    private void Awake()
    {
        _targetCache = _target;
    }

    private void OnValidate()
    {
        CheckTargetChanged();
    }

    private void CheckTargetChanged()
    { 
        if (_target == _targetCache)
            return;

        _targetCache = Target;
        TargetChanged?.Invoke(_target);
    }

    public Transform GetTarget()
    {
        return _target;
    }

    public bool HasTarget => _target != null;

    public float DistanceToTarget()
    {
        return Vector2.Distance(transform.position, _target.position);
    }

    public Vector3 DirectionToTarget()
    {
        Vector3 difference = _target.position - transform.position;
            return difference.normalized;
    }
}
