namespace Chinchillada.BehaviourSelections.BehaviourTree
{
    /// <summary>
    /// A composite behaviour that tries running each if his child behaviours until one succeeds.
    /// </summary>
    public class Selector : Composite
    {
        public Selector(BehaviourTree tree) : base(tree) { }

        protected int CurrentChildIndex { get; set; }

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
            if (childStatus == Status.Succes)
                Terminate(Status.Succes);

            //Terminate if it was the last child.
            CurrentChildIndex++;
            if (CurrentChildIndex >= Children.Count)
                Terminate(Status.Failure);

            //Start next child.
            StartCurrentChild();
        }
    }
}
