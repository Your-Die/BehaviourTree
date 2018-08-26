namespace Chinchillada.BehaviourSelections.BehaviourTree
{
    /// <summary>
    /// A composite behaviour that tries each of his child behaviours until one succeeds.
    /// </summary>
    public class Selector : Composite
    {
        /// <summary>
        /// Index of the currently active child.
        /// </summary>
        protected int CurrentChildIndex { get; set; }

        /// <summary>
        /// Construct a new <see cref="Selector"/>.
        /// </summary>
        /// <param name="tree">The tree this <see cref="IBehaviour"/> is a part of.</param>
        public Selector(BehaviourTree tree) : base(tree) { }

        /// <inheritdoc />
        protected override void Initialize()
        {
            base.Initialize();

            //Start first child.
            CurrentChildIndex = 0;
            StartCurrentChild();

            //Does nothing but wait for children to finish.
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
            currentChild.StartBehaviour();
        }

        /// <summary>
        /// Called when the current child is terminated.
        /// </summary>
        protected void OnCurrentChildTerminated(IBehaviour currentChild, Status childStatus)
        {
            //Unsubscribe.
            currentChild.Terminated -= OnCurrentChildTerminated;

            //Stop if the child failed.
            if (childStatus == Status.Success)
            {
                Terminate(Status.Success);
                return;
            }

            //Stop if it was the last child.
            CurrentChildIndex++;
            if (CurrentChildIndex < Children.Count)
                StartCurrentChild();
            else
                Terminate(Status.Failure);
        }
    }
}
