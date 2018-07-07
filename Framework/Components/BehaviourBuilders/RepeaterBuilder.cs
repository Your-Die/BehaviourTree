using System.Linq;
using UnityEngine;

namespace Chinchillada.BehaviourSelections.BehaviourTree.Builder
{
    /// <summary>
    /// Behaviour builder for <see cref="Repeater"/>.
    /// </summary>
    public class RepeaterBuilder : ParentBuilder
    {
        /// <summary>
        /// The amount of times to repeat the <see cref="Decorator.Child"/>.
        /// </summary>
        [SerializeField] private int _repeatCount = 4;

        /// <inheritdoc />
        public override IBehaviour Build(BehaviourTree tree)
        {
            IBehaviour child = BuildChildren(transform, tree).First();
            return new Repeater(tree, child, _repeatCount);
        }
    }
}