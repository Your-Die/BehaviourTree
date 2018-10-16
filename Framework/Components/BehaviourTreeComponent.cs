using UnityEngine;

namespace Chinchillada.BehaviourSelections.BehaviorTree.Builder
{
    /// <summary>
    /// Component that manages a behaviour tree build by <see cref="IBehaviourBuilder"/> on child objects.
    /// </summary>
    internal class BehaviourTreeComponent : MonoBehaviour
    {
        [SerializeField, HideInInspector] private BehaviourTree _behaviourTree;

        /// <summary>
        /// Wether the tree has been build or not.
        /// </summary>
        public bool IsBuild { get; private set; }

        /// <summary>
        /// Called when this object awakes.
        /// </summary>
        private void Awake()
        {
            //Build the tree.
            if(_behaviourTree == null)
                Build();
        }

        /// <summary>
        /// Updates the behaviour tree.
        /// </summary>
        private void Update()
        {
            if(IsBuild) _behaviourTree.Update();
        }

        /// <summary>
        /// Builds a <see cref="BehaviourTree"/> with the tree of child <see cref="IBehaviourBuilder"/>s.
        /// </summary>
        [ContextMenu("Build Tree")]
        public void Build()
        {
            //Find root builder.
            IBehaviourBuilder rootBuilder = GetComponentInChildren<IBehaviourBuilder>();
            if (rootBuilder == null)
            {
                Debug.LogWarning("No Behavior builder found as sibling of behaviour tree component.");
                enabled = false;
            }
            else
            {
                //Build the tree by building the root, which builds its children and so on.
                _behaviourTree = new BehaviourTree();
                IBehavior root = rootBuilder.Build(_behaviourTree);
                _behaviourTree.Root = root;

                IsBuild = true;
            }
        }
    }
}
