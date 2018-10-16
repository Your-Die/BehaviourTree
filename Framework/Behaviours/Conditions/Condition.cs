using System;

namespace Chinchillada.BehaviourSelections.BehaviorTree
{
    /// <summary>
    /// A behavior in a behavior tree that only checks something.
    /// </summary>
    public partial class Condition : Behavior
    {
        /// <summary>
        /// Interprets the result.
        /// </summary>
        private readonly IResultInterpreter _interpreter;

        /// <summary>
        /// The predicate that defines this condition.
        /// </summary>
        public Func<bool> Predicate { get; }

        /// <summary>
        /// The mode that we check in.
        /// </summary>
        public Mode CheckMode { get; }

        /// <summary>
        /// Construct a new Condition behaviour.
        /// </summary>
        /// <param name="tree">The <see cref="BehaviourTree"/> this <see cref="IBehavior"/> belongs to.</param>
        /// <param name="condition"><see cref="Predicate"/></param>
        /// <param name="mode"><see cref="Mode"/></param>
        public Condition(BehaviourTree tree, Func<bool> condition, Mode mode = Mode.CheckOnce) : base(tree)
        {
            Predicate = condition;
            CheckMode = mode;

            _interpreter = Interpreters[mode];
        }

        /// <inheritdoc />
        protected override Status UpdateInternal()
        {
            bool result = Predicate();
            return _interpreter.Interpret(result);
        }
    }
}
