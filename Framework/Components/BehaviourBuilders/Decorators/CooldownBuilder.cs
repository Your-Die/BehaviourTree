using System.Linq;
using UnityEngine;

namespace Chinchillada.BehaviourSelections.BehaviorTree.Builder
{
    internal class CooldownBuilder : DecoratorBuilder
    {
        [SerializeField] private float _cooldownDuration;
        [SerializeField] private bool _failOnCooldown;

        protected override IBehavior BuildDecorator(BehaviourTree tree, IBehavior child)
        {
            Timer cooldownTimer = new Timer(this, _cooldownDuration);
            return new CooldownDecorator(tree, child, cooldownTimer, _failOnCooldown);
        }
    }
}
