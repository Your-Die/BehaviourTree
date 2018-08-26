using System.Linq;
using UnityEngine;

namespace Chinchillada.BehaviourSelections.BehaviourTree.Builder
{
    internal class CooldownBuilder : DecoratorBuilder
    {
        [SerializeField] private float _cooldownDuration;
        [SerializeField] private bool _failOnCooldown;

        protected override IBehaviour BuildDecorator(BehaviourTree tree, IBehaviour child)
        {
            Timer cooldownTimer = new Timer(this, _cooldownDuration);
            return new CooldownDecorator(tree, child, cooldownTimer, _failOnCooldown);
        }
    }
}
