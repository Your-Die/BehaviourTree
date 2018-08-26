using System;
using System.Collections.Generic;
using UnityEngine;
using Status = Chinchillada.BehaviourSelections.BehaviourTree.Behaviour.Status;

namespace Chinchillada.BehaviourSelections.BehaviourTree.Builder
{
    internal class StatusInterpreterBuilder : DecoratorBuilder
    {
        [SerializeField] private StatusMap[] _mapping;
        
        protected override IBehaviour BuildDecorator(BehaviourTree tree, IBehaviour child)
        {
            Dictionary<Status, Status> dictionary = BuildDictionary();
            return new StatusInterpreter(tree, child, dictionary);
        }

        private Dictionary<Status, Status> BuildDictionary()
        {
            Dictionary<Status, Status> dictionary = new Dictionary<Status, Status>();

            foreach (StatusMap statusMap in _mapping)
                dictionary[statusMap.From] = statusMap.To;

            return dictionary;
        }

        [Serializable]
        private struct StatusMap
        {
            public Status From;
            public Status To;
        }
    }
}
