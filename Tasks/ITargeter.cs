using System;
using UnityEngine;

namespace Chinchillada.BehaviourSelections.BehaviourTree.Tasks
{
    internal interface ITargeter
    {
        event Action<Transform> TargetChanged;

        bool TryGetTarget(out Transform target);
    }
}
