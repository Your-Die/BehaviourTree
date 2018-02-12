using System;
using UnityEngine;

namespace Chinchillada.BehaviourSelections.BehaviourTree.Tasks
{
    internal interface ITargeter
    {
        float DistanceToTarget();

        event Action<Transform> TargetChanged;

        Transform GetTarget();
    }
}
