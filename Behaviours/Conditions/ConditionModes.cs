using System.Collections.Generic;

namespace Chinchillada.BehaviourSelections.BehaviourTree
{
    /// <summary>
    /// Partial class that contains the implementations of the different condition modes.
    /// </summary>
    public partial class Condition
    {
        /// <summary>
        /// Dictionary for the interpreters for each mode.
        /// </summary>
        private static readonly Dictionary<Mode, IResultIntepreter> Interpreters =
            new Dictionary<Mode, IResultIntepreter>
            {
                {Mode.CheckOnce, new InstantCheck() },
                {Mode.Monitoring, new Monitor() }
            };

        public enum Mode
        {
            /// <summary>
            /// The behaviour terminates after the first update, returning the result.
            /// </summary>
            CheckOnce,

            /// <summary>
            /// The behaviour keeps running until the condition fails.
            /// </summary>
            Monitoring
        }

        /// <summary>
        /// Interface for classes that interpret condition results.
        /// </summary>
        private interface IResultIntepreter
        {
            Status Interpret(bool result);
        }

        /// <summary>
        /// Checks the result and makes the behaviour terminate immediately.
        /// </summary>
        private class InstantCheck : IResultIntepreter
        {
            public Status Interpret(bool result)
            {
                return result ? Status.Succes : Status.Failure;
            }
        }

        /// <summary>
        /// Keeps the behaviour active until the condition fails.
        /// </summary>
        private class Monitor : IResultIntepreter
        {
            public Status Interpret(bool result)
            {
                return result ? Status.Running : Status.Failure;
            }
        }
    }
}
