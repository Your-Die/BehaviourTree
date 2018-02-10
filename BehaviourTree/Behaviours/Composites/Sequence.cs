namespace Chinchillada.BehaviourSelections.BehaviourTree
{
    /// <summary>
    /// A composite behaviour that runs its child behaviour subsequently.
    /// </summary>
    public class Sequence : Composite
    {
        /// <summary>
        /// The index we're currently at.
        /// </summary>
        public int CurrentChildIndex { get; private set; }

        public Sequence(BehaviourTree tree) : base(tree) { }

        /// <summary>
        /// Initialize the sequence by activating the first behaviour.
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();
            CurrentChildIndex = 0;

            StartCurrentChild();
            Suspend();
        }

        /// <summary>
        /// Starts the child corresponding to the <see cref="CurrentChildIndex"/>.
        /// </summary>
        private void StartCurrentChild()
        {
            //Get the current child and subscribe to it's termination event.
            IBehaviour currentChild = Children[CurrentChildIndex];
            currentChild.Terminated += OnCurrentChildTerminated;

            //Start.
            Tree.StartBehaviour(currentChild);
        }

        /// <summary>
        /// Called when the current child is terminated.
        /// </summary>
        private void OnCurrentChildTerminated(IBehaviour currentChild, Status childStatus)
        {
            //Unsubscribe.
            currentChild.Terminated -= OnCurrentChildTerminated;

            //Stop if child failed.
            if (childStatus == Status.Failure)
                Terminate(Status.Failure);

            //Terminate if it was the last child.
            CurrentChildIndex++;
            if (CurrentChildIndex >= Children.Count)
                Terminate(Status.Succes);

            //Start next child.
            StartCurrentChild();
        }
    }
}
