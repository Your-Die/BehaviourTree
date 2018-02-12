using System.Collections.Generic;
using System.Linq;
using Chinchillada.BehaviourSelections.Utilities;

namespace Chinchillada.BehaviourSelections.BehaviourTree
{
    public class BehaviourTree
    {
        /// <summary>
        /// The behaviours that are currently active.
        /// </summary>
        private readonly LinkedList<IBehaviour> _activeBehaviours = new LinkedList<IBehaviour>();
        private readonly List<IBehaviour> _suspendedBehaviours = new List<IBehaviour>();
        /// <summary>
        /// The root behaviour in this tree.
        /// </summary>
        public IBehaviour Root { get; set; }

        public BehaviourTree() { }

        /// <summary>
        /// Construct a new <see cref="BehaviourTree"/> with <paramref name="root"/> as the root.
        /// </summary>
        public BehaviourTree(IBehaviour root)
        {
            Root = root;
        }

        /// <summary>
        /// Updates the active behaviours in the behaviour tree.
        /// </summary>
        public void Update()
        {
            if (!_activeBehaviours.Any() && _suspendedBehaviours.Any())
                _activeBehaviours.AddLast(Root);

            _activeBehaviours.AddLast((IBehaviour)null);

            bool running;
            do
            {
                running = Step();
            } while (running);
        }

        /// <summary>
        /// Update the next active behaviour.
        /// </summary>
        /// <returns></returns>
        public bool Step()
        {
            IBehaviour current = _activeBehaviours.GrabFirst();

            if (current == null)
                return false;

            Behaviour.Status status = current.Tick();

            if (!current.IsTerminated && status != Behaviour.Status.Suspended)
                _activeBehaviours.AddLast(current);

            return true;
        }

        /// <summary>
        /// Activate the <paramref name="behaviour"/>.
        /// </summary>
        public void StartBehaviour(IBehaviour behaviour)
        {
            _activeBehaviours.AddFirst(behaviour);
        }

        /// <summary>
        /// Stop the <paramref name="behaviour"/>.
        /// </summary>
        /// <param name="behaviour">The <see cref="IBehaviour"/> we want to stop.</param>
        /// <param name="status">The status to stop the behaviour with.</param>
        public void StopBehaviour(IBehaviour behaviour, Behaviour.Status status)
        {
            behaviour.Terminate(status);
        }

        /// <summary>
        /// Suspends the <paramref name="behaviour"/>.
        /// </summary>
        public void SuspendBehaviour(IBehaviour behaviour)
        {
            _activeBehaviours.Remove(behaviour);
            _suspendedBehaviours.Add(behaviour);

            behaviour.Terminated += OnSuspendedBehaviourTerminated;
        }

        private void OnSuspendedBehaviourTerminated(IBehaviour behaviour, Behaviour.Status status)
        {
            _suspendedBehaviours.Remove(behaviour);
            behaviour.Terminated -= OnSuspendedBehaviourTerminated;
        }
    }
}
