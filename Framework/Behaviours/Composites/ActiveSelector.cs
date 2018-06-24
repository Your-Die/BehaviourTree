namespace Chinchillada.BehaviourSelections.BehaviourTree
{
    /// <summary>
    /// Selector that validates the selection each update.
    /// </summary>
    public class ActiveSelector : Selector
    {
        /// <summary>
        /// Index of the child that was selected the previous update.
        /// </summary>
        private int _previousChildIndex;

        public ActiveSelector(BehaviourTree tree) : base(tree) { }

        protected override void Initialize()
        {
            _previousChildIndex = -1;
            Start();
        }

        /// <summary>
        /// Re-initialize the base Selector so it revaluates the selection.
        /// </summary>
        private void Start()
        {
            base.Initialize();
            Continue();
        }

        protected override Status UpdateInternal()
        {
            //Check if we reached the previously active child. If not, cancel it.
            if (CurrentChildIndex < _previousChildIndex)
                Children[_previousChildIndex].Terminate(Status.Failure);

            //Save currently active child.
            _previousChildIndex = CurrentChildIndex;
            Children[_previousChildIndex].Terminated -= OnCurrentChildTerminated;

            Start();
            return Status.Running;
        }
    }
}
