using System;
using System.Collections.Generic;
using System.Linq;

namespace Chinchillada.BehaviourSelections.BehaviourTree
{
    /// <summary>
    /// A <see cref="Composite"/> behaviour that updates every non-terminated child behaviour every frame, 
    /// until either the <see cref="SuccessPolicy"/> or the <see cref="FailurePolicy"/> is reached.
    /// </summary>
    public class Parallel : Composite
    {
        /// <summary>
        /// The policy for what counts as a success.
        /// </summary>
        public Policy SuccessPolicy { get; }

        /// <summary>
        /// The policy for what counts as a failure.
        /// </summary>
        public Policy FailurePolicy { get; }

        private LinkedList<IBehaviour> _activeChildren;

        /// <summary>
        /// The sucessful children up until now.
        /// </summary>
        private readonly HashSet<IBehaviour> _succesfulChildren = new HashSet<IBehaviour>();

        /// <summary>
        /// The failed children up until now.
        /// </summary>
        private readonly HashSet<IBehaviour> _failedChildren = new HashSet<IBehaviour>();

        /// <summary>
        /// The possible policies.
        /// </summary>
        public enum Policy
        {
            /// <summary>
            /// Policy for when only one result is necessary.
            /// </summary>
            RequireOne,

            /// <summary>
            /// Policy for when all results are necessary.
            /// </summary>
            RequireAll
        }

        /// <summary>
        /// Construct a new Parralel composite behaviour.
        /// </summary>
        /// <param name="tree">The tree this behaviour belongs in.</param>
        /// <param name="successPolicy">The success policy.</param>
        /// <param name="failurePolicy">The failure policy.</param>
        public Parallel(BehaviourTree tree, Policy successPolicy, Policy failurePolicy)
            : base(tree)
        {
            if (successPolicy == Policy.RequireAll && successPolicy == failurePolicy)
                throw new ArgumentException("Success and failure policies can't both be require-all, will never terminate.");

            SuccessPolicy = successPolicy;
            FailurePolicy = failurePolicy;
        }

        /// <inheritdoc />
        protected override void Initialize()
        {
            base.Initialize();

            // Initialize the collections of children.
            _activeChildren = new LinkedList<IBehaviour>(Children);
            _succesfulChildren.Clear();
            _failedChildren.Clear();

            //Start each child behaviour.
            foreach (IBehaviour child in Children)
            {
                child.Terminated += OnChildTerminated;
                Tree.StartBehaviour(child);
            }

            //Suspend.
            Suspend();
        }

        /// <inheritdoc />
        public override void Terminate()
        {
            base.Terminate();

            //Stop any remaining active behaviours.
            foreach (IBehaviour child in _activeChildren)
            {
                child.Terminated -= OnChildTerminated;
                Tree.StopBehaviour(child, CurrentStatus);
            }
        }

        /// <summary>
        /// Called when the <paramref name="child"/> has terminated.
        /// </summary>
        /// <param name="child">The child that has terminated.</param>
        /// <param name="status">The status the <paramref name="child"/> termianted with.</param>
        private void OnChildTerminated(IBehaviour child, Status status)
        {
            //Remove from active list.
            child.Terminated -= OnChildTerminated;
            _activeChildren.Remove(child);

            //Check if either policy is reached.
            switch (status)
            {
                case Status.Succes:
                    _succesfulChildren.Add(child);
                    if (ValidatePolicy(SuccessPolicy, _succesfulChildren))
                        Terminate(Status.Succes);

                    break;
                case Status.Failure:
                    _failedChildren.Add(child);
                    if (ValidatePolicy(FailurePolicy, _failedChildren))
                        Terminate(Status.Failure);

                    break; 
            } 
        }

        /// <summary>
        /// Validates if the <paramref name="policy"/> is satisfied by the <paramref name="policySet"/>.
        /// </summary>
        private bool ValidatePolicy(Policy policy, ICollection<IBehaviour> policySet)
        {
            switch (policy)
            {
                case Policy.RequireOne:
                    return policySet.Any();
                case Policy.RequireAll:
                    return policySet.Count >= Children.Count;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
