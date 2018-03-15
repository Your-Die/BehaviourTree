using UnityEngine;

namespace Chinchillada.BehaviourSelections.BehaviourTree.Builder
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

        [ContextMenu("Build Tree")]
        public void Build()
        {
            IBehaviourBuilder rootBuilder = GetComponent<IBehaviourBuilder>();
            if (rootBuilder == null)
            {
                Debug.LogWarning("No Behaviour builder found as sibling of behaviour tree component.");
                enabled = false;
            }
            else
            {
                _behaviourTree = new BehaviourTree();
                IBehaviour root = rootBuilder.Build(_behaviourTree);
                _behaviourTree.Root = root;

                IsBuild = true;
            }
        }
    }
}
