using UnityEngine.Events;

namespace Chinchillada.BehaviourSelections.BehaviourTree.Builder
{
    internal class UnityActionBuilder : ActionBuilder
    {
        public UnityEvent Action;

        protected override bool DoAction()
        {
            Action.Invoke();
            return true;
        }
    }
}
