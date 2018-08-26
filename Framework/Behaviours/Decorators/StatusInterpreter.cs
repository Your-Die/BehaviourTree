using System.Collections.Generic;

namespace Chinchillada.BehaviourSelections.BehaviourTree
{
    internal class StatusInterpreter : Decorator
    {
        private readonly Dictionary<Status, Status> _mapping;

        public StatusInterpreter(BehaviourTree tree, IBehaviour child, Dictionary<Status, Status> mapping) : base(tree, child)
        {
            _mapping = mapping;
        }

        protected override void OnChildTerminated(IBehaviour child, Status status)
        {
            Status result;

            if (!_mapping.TryGetValue(status, out result))
                result = status;

            Terminate(result);
        }
    }
}
