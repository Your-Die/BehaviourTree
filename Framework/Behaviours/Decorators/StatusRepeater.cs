namespace Chinchillada.BehaviourSelections.BehaviorTree
{
    /// <summary>
    /// <see cref="Decorator"/> that repeats it's child <see cref="IBehavior"/> if it terminated with the <see cref="_repeatStatus"/>.
    /// </summary>
    internal class StatusRepeater : Decorator
    {
        /// <summary>
        /// Status for which we repeat the child.
        /// </summary>
        private readonly Status _repeatStatus;

        /// <summary>
        /// Construct a new <see cref="StatusRepeater"/>.
        /// </summary>
        /// <param name="tree">The tree that this <see cref="IBehavior"/> is a part of.</param>
        /// <param name="child">The child that this <see cref="Decorator"/> decorates.</param>
        /// <param name="repeatStatus">The terminating status for which we will repeat the child.</param>
        public StatusRepeater(BehaviourTree tree, IBehavior child, Status repeatStatus) : base(tree, child)
        {
            _repeatStatus = repeatStatus;
        }

        /// <inheritdoc />
        protected override void Initialize()
        {
            base.Initialize();
            
            //Start child.
            Child.Terminated += OnChildTerminated;
            Child.StartBehaviour();

            Suspend();
        }

        /// <summary>
        /// Called when the <paramref name="child"/> is terminated.
        /// </summary>
        /// <param name="child">The child.</param>
        /// <param name="status">The status that the <paramref name="child"/> terminated with.</param>
        protected override void OnChildTerminated(IBehavior child, Status status)
        {
            if(status == _repeatStatus)
                child.StartBehaviour();
            else
                Terminate(status);
        }
    }
}
