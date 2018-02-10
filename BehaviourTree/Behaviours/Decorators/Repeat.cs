namespace Chinchillada.BehaviourSelections.BehaviourTree
{
    /// <summary>
    /// A decorator behaviour that repeats its child behaviour a specified amount of times.
    /// </summary>
    public class Repeat : Decorator
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
        public Repeat(BehaviourTree tree, IBehaviour child, int count) : base(tree, child)
        {
            _count = count;
        }

        /// <inheritdoc />
        protected override Status UpdateInternal()
        {
            //Repeat for the given amount of times.
            while (_currentCount < _count)
            {
                //Run child.
                Status childStatus = Child.Update();

                //If it's not a success, return the status.
                if (childStatus != Status.Succes)
                    return childStatus;

                //If it's a succes, increment the counter.
                _currentCount++;
            }

            return Status.Succes;
        }
    }
}
