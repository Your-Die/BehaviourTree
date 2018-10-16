using System.Collections.Generic;
using UnityEngine;

namespace Chinchillada.BehaviourSelections.BehaviorTree.Builder
{
    /// <summary>
    /// Base abstract class for builders of <see cref="IConditionalComposite"/> behaviors.
    /// </summary>
    [ExecuteInEditMode]
    internal abstract class ConditionalCompositeBuilder : CompositeBuilder
    {
        /// <summary>
        /// Parent object for condition behaviors.
        /// </summary>
        [SerializeField] private GameObject _conditionsParent;

        /// <summary>
        /// Parent object for action behaviors.
        /// </summary>
        [SerializeField] private GameObject _actionsParent;

        /// <summary>
        /// Called when this object first awakens.
        /// Ensures the child object groupers exist.
        /// </summary>
        protected virtual void Awake()
        {
            if (_conditionsParent == null)
                _conditionsParent = CreateEmptyChild("Conditions");

            if(_actionsParent == null)
                _actionsParent = CreateEmptyChild("Actions");
        }

        /// <summary>
        /// Creates an empty child with the given <paramref name="childName"/>.
        /// </summary>
        /// <param name="childName">Name for the child.</param>
        /// <returns>The child.</returns>
        private GameObject CreateEmptyChild(string childName)
        {
            GameObject child = new GameObject(childName);
            child.transform.parent = transform;

            return child;
        }

        /// <inheritdoc />
        protected override void InitializeChildren(Composite composite, BehaviourTree tree)
        {
            //Cast to conditional.
            IConditionalComposite conditionalComposite = (IConditionalComposite) composite;

            //Build children.
            IEnumerable<IBehavior> conditions = BuildChildren(_conditionsParent.transform, tree);
            IEnumerable<IBehavior> actions = BuildChildren(_actionsParent.transform, tree);

            //Add children.
            foreach (IBehavior condition in conditions)
                conditionalComposite.AddCondition(condition);
            foreach (IBehavior action in actions)
                conditionalComposite.AddAction(action);
        }
    }
}
