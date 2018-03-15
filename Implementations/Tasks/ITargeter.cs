using System;
using UnityEngine;

namespace Chinchillada.BehaviourSelections.BehaviourTree.Tasks
{
    internal interface ITargeter
    {
        event Action<Transform> TargetChanged;
        Transform GetTarget();
        float DistanceToTarget();
        Vector3 DirectionToTarget();
    } 
}
