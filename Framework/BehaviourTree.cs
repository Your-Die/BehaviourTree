namespace Chinchillada.BehaviourSelections.BehaviorTree
{
    using System.Collections.Generic;

    using Chinchillada.BehaviourSelections.Utilities;

    /// <summary>
    /// A tree structure that selects what behavior to use.
    /// </summary>
    public class BehaviourTree
    {
        /// <summary>
        /// The behaviors that are currently active.
        /// </summary>
        private readonly LinkedList<IBehavior> activeBehaviors = new LinkedList<IBehavior>();

        /// <summary>
        /// The behaviors that have been suspended. These aren't updated, but the tree is still seen as active if this isn't empty.
        /// </summary>
        private readonly List<IBehavior> suspendedBehaviors = new List<IBehavior>();

        /// <summary>
        /// Gets or sets the root behavior in this tree.  
        /// </summary>
        public IBehavior Root { get; set; }

        /// <summary>
        /// Updates the active behaviors in the behavior tree.
        /// </summary>
        public void Update()
        {
            if (this.activeBehaviors.IsEmpty() && this.suspendedBehaviors.IsEmpty())
            {
                this.activeBehaviors.AddLast(this.Root);
            }

            this.activeBehaviors.AddLast((IBehavior)null);

            bool running;
            do
            {
                running = this.Step();
            }
            while (running);
        }

        /// <summary>
        /// Update the next active behavior.
        /// </summary>
        /// <returns>Whether we can step again.</returns>
        public bool Step()
        {
            IBehavior current = this.activeBehaviors.GrabFirst();

            if (current == null)
            {
                return false;
            }

            Behavior.Status result = current.Tick();

            if (result == Behavior.Status.Running)
            {
                this.activeBehaviors.AddLast(current);
            }

            return true;
        }

        /// <summary>
        /// Activate the <paramref name="behavior"/>.
        /// </summary>
        /// <param name="behavior">
        /// The behavior.
        /// </param>
        public void StartBehaviour(IBehavior behavior) => this.activeBehaviors.AddFirst(behavior);

        /// <summary>
        /// Activates the <paramref name="behavior"/> the next frame.
        /// </summary>
        /// <param name="behavior">
        /// The behavior.
        /// </param>
        public void StartNextFrame(IBehavior behavior) => this.activeBehaviors.AddLast(behavior);

        /// <summary>
        /// Stop the <paramref name="behavior"/>.
        /// </summary>
        /// <param name="behavior">The <see cref="IBehavior"/> we want to stop.</param>
        /// <param name="status">The status to stop the behavior with.</param>
        public void StopBehaviour(IBehavior behavior, Behavior.Status status)
        {
            behavior.Terminate(status);
            this.activeBehaviors.Remove(behavior);
        }

        /// <summary>
        /// Suspends the <paramref name="behavior"/>.
        /// </summary>
        /// <param name="behavior">
        /// The behavior.
        /// </param>
        public void SuspendBehaviour(IBehavior behavior)
        {
            this.activeBehaviors.Remove(behavior);
            this.suspendedBehaviors.Add(behavior);

            behavior.Terminated += this.OnSuspendedBehaviourTerminated;
        }

        /// <summary>
        /// Continues the behavior if it was suspended.
        /// </summary>
        /// <param name="suspendedBehavior">
        /// The behavior.
        /// </param>
        public void ContinueBehaviour(IBehavior suspendedBehavior)
        {
            if (!this.suspendedBehaviors.Remove(suspendedBehavior))
            {
                return;
            }

            this.activeBehaviors.AddLast(suspendedBehavior);
            suspendedBehavior.Terminated -= this.OnSuspendedBehaviourTerminated;
        }

        /// <summary>
        /// Called when a suspended <see cref="IBehavior"/> has terminated.
        /// Removes it from <see cref="suspendedBehaviors"/>.
        /// </summary>
        /// <param name="behavior">The suspended behavior that has been terminated.</param>
        /// <param name="status">The status the <paramref name="behavior"/> has terminated with.</param>
        private void OnSuspendedBehaviourTerminated(IBehavior behavior, Behavior.Status status)
        {
            this.suspendedBehaviors.Remove(behavior);
            behavior.Terminated -= this.OnSuspendedBehaviourTerminated;
        }
    }
}
