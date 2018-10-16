using UnityEngine;

namespace Chinchillada.BehaviourSelections.BehaviorTree.Builder
{
    /// <summary>
    /// Base class for implementers of the <see cref="IBehaviourBuilder"/> interface that enforces the type name in the object name
    /// in the editor.
    /// </summary>
    public abstract class BehaviourBuilder : MonoBehaviour, IBehaviourBuilder
    {
        /// <summary>
        /// Name that is enforced in the <see cref="GameObject.name"/>
        /// </summary>
        protected abstract string TypeName { get; }

        /// <summary>
        /// Ensure the type name of the Tree element is included.
        /// </summary>
        protected virtual void OnValidate()
        {
            var lowerName = name.ToLower();
            var lowerType = TypeName.ToLower();

            if (!lowerName.Contains(lowerType))
                name += $" ({TypeName})";
        }

        /// <inheritdoc />
        public abstract IBehavior Build(BehaviourTree tree);
    }
}
