using System;
using System.Collections.Generic;
using System.Linq;

namespace Chinchillada.BehaviourSelections.BehaviourTree
{
    public class Parallel : Composite
    {
        /// <summary>
        /// The policy for what counts as a success.
        /// </summary>
        private readonly Policy _successPolicy;

        /// <summary>
        /// The policy for what counts as a failure.
        /// </summary>
        private readonly Policy _failurePolicy;

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

            _successPolicy = successPolicy;
            _failurePolicy = failurePolicy;
        }

        protected override void Initialize()
        {
            base.Initialize();

            _activeChildren = new LinkedList<IBehaviour>(Children);
            _succesfulChildren.Clear();
            _failedChildren.Clear();

            foreach (IBehaviour child in Children)
            {
                child.Terminated += OnChildTerminated;
                Tree.StartBehaviour(child);
            }

            Suspend();
        }

        public override void Terminate()
        {
            base.Terminate();

            foreach (IBehaviour activeChild in _activeChildren)
                Tree.StopBehaviour(activeChild, CurrentStatus);
        }

        private void OnChildTerminated(IBehaviour child, Status status)
        {
            _activeChildren.Remove(child);

            switch (status)
            {
                case Status.Succes:
                    _succesfulChildren.Add(child);
                    if (ValidatePolicy(_successPolicy, _succesfulChildren))
                        Terminate(Status.Succes);

                    break;
                case Status.Failure:
                    _failedChildren.Add(child);
                    if (ValidatePolicy(_failurePolicy, _failedChildren))
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
