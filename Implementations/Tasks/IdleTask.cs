using UnityEngine;
using Status = Chinchillada.BehaviourSelections.BehaviourTree.Behaviour.Status;

namespace Chinchillada.BehaviourSelections.BehaviourTree.Tasks
{
    internal class IdleTask : Task
    {
        [SerializeField] private float _duration; 

        protected override void OnInitialization()
        {
            Behaviour.Suspend();
            Invoke(nameof(Finish), _duration);
        }

        protected override Status UpdateInternal()
        {
            return Behaviour.Status;
        }

        private void Finish()
        {
            Behaviour.Terminate(Status.Succes);
        }
    }
}
