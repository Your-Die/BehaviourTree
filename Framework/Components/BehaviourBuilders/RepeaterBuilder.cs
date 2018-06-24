using System.Linq;
using UnityEngine;

namespace Chinchillada.BehaviourSelections.BehaviourTree.Builder
{
    public class RepeaterBuilder : ParentBuilder
    {
        [SerializeField] private int _repeatCount = 4;

        public override IBehaviour Build(BehaviourTree tree)
        {
            IBehaviour child = BuildChildren(transform, tree).First();
            return new Repeater(tree, child, _repeatCount);
        }
    }
}