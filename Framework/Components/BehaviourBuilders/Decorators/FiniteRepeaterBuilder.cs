using UnityEngine;

namespace Chinchillada.BehaviourSelections.BehaviorTree.Builder
{
    /// <summary>
    /// Behavior builder for <see cref="FiniteRepeater"/>.
    /// </summary>
    public class FiniteRepeaterBuilder : DecoratorBuilder
    {
        /// <summary>
        /// The amount of times to repeat the <see cref="Decorator.Child"/>.
        /// </summary>
        [SerializeField] private int _repeatCount = 4;

        protected override IBehavior BuildDecorator(BehaviourTree tree, IBehavior child)
        {
            return new FiniteRepeater(tree, child, _repeatCount);
        }
    }
}