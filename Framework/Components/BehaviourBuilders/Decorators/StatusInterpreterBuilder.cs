using System;
using System.Collections.Generic;
using UnityEngine;

namespace Chinchillada.BehaviourSelections.BehaviorTree.Builder
{
    internal class StatusInterpreterBuilder : DecoratorBuilder
    {
        [SerializeField] private StatusMap[] _mapping;
        
        protected override IBehavior BuildDecorator(BehaviourTree tree, IBehavior child)
        {
            Dictionary<Behavior.Status, Behavior.Status> dictionary = BuildDictionary();
            return new StatusInterpreter(tree, child, dictionary);
        }

        private Dictionary<Behavior.Status, Behavior.Status> BuildDictionary()
        {
            Dictionary<Behavior.Status, Behavior.Status> dictionary = new Dictionary<Behavior.Status, Behavior.Status>();

            foreach (StatusMap statusMap in _mapping)
                dictionary[statusMap.From] = statusMap.To;

            return dictionary;
        }

        [Serializable]
        private struct StatusMap
        {
            public Behavior.Status From;
            public Behavior.Status To;
        }
    }
}
