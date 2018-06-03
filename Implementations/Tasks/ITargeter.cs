using System;
using UnityEngine;

namespace Chinchillada.BehaviourSelections.BehaviourTree.Tasks
{
    /// <summary>
    /// Interface for components that handle finding and keeping track of any potential targets.
    /// </summary>
    internal interface ITargeter
    {
        /// <summary>
        /// Event invoked when the active target has changed.
        /// </summary>
        event Action<Transform> TargetChanged;

        /// <summary>
        /// Get the current active target.
        /// If no valid targets are found, will return null.
        /// </summary>
        Transform GetTarget();

        /// <summary>
        /// Checks if we currently have a target.
        /// </summary>
        /// <returns></returns>
        bool HasTarget();

        /// <summary>
        /// Get the current distance to the target.
        /// </summary> 
        float DistanceToTarget();

        /// <summary>
        /// Get the direction to the current target.
        /// </summary> s
        Vector3 DirectionToTarget();
    } 
}
