using System.Collections.Generic;
using System.Linq;
using Chinchillada.BehaviourSelections.Utilities;

namespace Chinchillada.BehaviourSelections.BehaviorTree
{
    public class BehaviourTree
    {
        /// <summary>
        /// The behaviours that are currently active.
        /// </summary>
        private readonly LinkedList<IBehavior> _activeBehaviours = new LinkedList<IBehavior>();

        /// <summary>
        /// The behaviours that have been suspended. These aren't updated, but the tree is still seen as active if this isn't empty.
        /// </summary>
        private readonly List<IBehavior> _suspendedBehaviours = new List<IBehavior>();
        /// <summary>
        /// The root behavior in this tree.
        /// </summary>
        public IBehavior Root { get; set; }

        /// <summary>
        /// Construct a new empty <see cref="BehaviourTree"/>.
        /// </summary>
        public BehaviourTree() { }

        /// <summary>
        /// Construct a new <see cref="BehaviourTree"/> with <paramref name="root"/> as the root.
        /// </summary>
        public BehaviourTree(IBehavior root)
        {
            Root = root;
        }

        /// <summary>
        /// Updates the active behaviours in the behavior tree.
        /// </summary>
        public void Update()
        {
            if (_activeBehaviours.IsEmpty() && _suspendedBehaviours.IsEmpty())
                _activeBehaviours.AddLast(Root);

            _activeBehaviours.AddLast((IBehavior)null);

            bool running;
            do
            {
                running = Step();
            } while (running);
        }

        /// <summary>
        /// Update the next active behavior.
        /// </summary>
        /// <returns></returns>
        public bool Step()
        {
            IBehavior current = _activeBehaviours.GrabFirst();

            if (current == null)
                return false;

            Behavior.Status result = current.Tick();

            if (result == Behavior.Status.Running)
                _activeBehaviours.AddLast(current);

            return true;
        }

        /// <summary>
        /// Activate the <paramref name="behavior"/>.
        /// </summary>
        public void StartBehaviour(IBehavior behavior)
        {
            _activeBehaviours.AddFirst(behavior);
        }

        /// <summary>
        /// Activates the <paramref name="behavior"/> the next frame.
        /// </summary>
        /// <param name="behavior"></param>
        public void StartNextFrame(IBehavior behavior)
        {
            _activeBehaviours.AddLast(behavior);
        }

        /// <summary>
        /// Stop the <paramref name="behavior"/>.
        /// </summary>
        /// <param name="behavior">The <see cref="IBehavior"/> we want to stop.</param>
        /// <param name="status">The status to stop the behavior with.</param>
        public void StopBehaviour(IBehavior behavior, Behavior.Status status)
        {
            behavior.Terminate(status);
            _activeBehaviours.Remove(behavior);
        }

        /// <summary>
        /// Suspends the <paramref name="behavior"/>.
        /// </summary>
        public void SuspendBehaviour(IBehavior behavior)
        {
            _activeBehaviours.Remove(behavior);
            _suspendedBehaviours.Add(behavior);

            behavior.Terminated += OnSuspendedBehaviourTerminated;
        }

        public void ContinueBehaviour(IBehavior suspendedBehavior)
        {
            if (!_suspendedBehaviours.Remove(suspendedBehavior))
                return;

            _activeBehaviours.AddLast(suspendedBehavior);
            suspendedBehavior.Terminated -= OnSuspendedBehaviourTerminated;
        }

        /// <summary>
        /// Called when a suspended <see cref="IBehavior"/> has terminated.
        /// Removes it from <see cref="_suspendedBehaviours"/>.
        /// </summary>
        /// <param name="behavior">The suspended behavior that has been terminated.</param>
        /// <param name="status">The status the <paramref name="behavior"/> has terminated with.</param>
        private void OnSuspendedBehaviourTerminated(IBehavior behavior, Behavior.Status status)
        {
            _suspendedBehaviours.Remove(behavior);
            behavior.Terminated -= OnSuspendedBehaviourTerminated;
        }
    }
}
