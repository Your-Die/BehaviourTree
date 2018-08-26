using System.Linq;
using UnityEngine;

namespace Chinchillada.BehaviourSelections.BehaviourTree.Builder
{
    /// <summary>
    /// Behaviour builder for <see cref="FiniteRepeater"/>.
    /// </summary>
    public class FiniteRepeaterBuilder : DecoratorBuilder
    {
        /// <summary>
        /// The amount of times to repeat the <see cref="Decorator.Child"/>.
        /// </summary>
        [SerializeField] private int _repeatCount = 4;

        protected override IBehaviour BuildDecorator(BehaviourTree tree, IBehaviour child)
        {
            return new FiniteRepeater(tree, child, _repeatCount);
        }
    }
}