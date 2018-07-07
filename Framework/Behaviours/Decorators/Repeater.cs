namespace Chinchillada.BehaviourSelections.BehaviourTree
{
    /// <summary>
    /// A decorator behaviour that repeats its child behaviour a specified amount of times.
    /// </summary>
    public class Repeater : Decorator
    {
        /// <summary>
        /// The amount of times to repeat the <see cref="Decorator.Child"/>.
        /// </summary>
        private readonly int _count;

        /// <summary>
        /// The amount of times we've already repeated the <see cref="Decorator.Child"/>.
        /// </summary>
        private int _currentCount;

        /// <summary>
        /// Constructs a new Repeat decorator behaviour.
        /// </summary>
        /// <param name="tree">The <see cref="BehaviourTree"/> this <see cref="IBehaviour"/> belongs to.</param>
        /// <param name="child">The child behaviour we want to repeat.</param>
        /// <param name="count">The amount of times we want to repeat the <paramref name="child"/>.</param>
        public Repeater(BehaviourTree tree, IBehaviour child, int count) : base(tree, child)
        {
            _count = count;
        }

        /// <inheritdoc />
        protected override void Initialize()
        {
            base.Initialize();

            //Reset counter.
            _currentCount = 0;

            //Start child.
            Child.Terminated += OnChildTerminated;
            Child.StartBehaviour();
        }

        /// <inheritdoc />
        public override void Terminate()
        {
            //Unsubscribe from child.
            Child.Terminated -= OnChildTerminated;

            //Ensure child is also terminated.
            if(!Child.IsTerminated)
                Child.Terminate(CurrentStatus);

            base.Terminate();
        }

        /// <summary>
        /// Called when the <paramref name="child"/> is terminated.
        /// </summary>
        /// <param name="child">The child.</param>
        /// <param name="status">The status that the <paramref name="child"/> terminated with.</param>
        private void OnChildTerminated(IBehaviour child, Status status)
        {
            //Increment and check if we repeated enough.
            _currentCount++;
            if (_currentCount >= _count)
            {
                Terminate(Status.Succes);
                return;
            }

            //Repeat.
            Child.StartBehaviour();
        }


    }
}
