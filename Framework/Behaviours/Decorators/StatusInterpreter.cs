using System.Collections.Generic;

namespace Chinchillada.BehaviourSelections.BehaviorTree
{
    internal class StatusInterpreter : Decorator
    {
        private readonly Dictionary<Status, Status> _mapping;

        public StatusInterpreter(BehaviourTree tree, IBehavior child, Dictionary<Status, Status> mapping) : base(tree, child)
        {
            _mapping = mapping;
        }

        protected override void OnChildTerminated(IBehavior child, Status status)
        {
            Status result;

            if (!_mapping.TryGetValue(status, out result))
                result = status;

            Terminate(result);
        }
    }
}
