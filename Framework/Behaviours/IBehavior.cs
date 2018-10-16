using System;

namespace Chinchillada.BehaviourSelections.BehaviorTree
{
    public interface IBehavior
    {
        /// <summary>
        /// Event invoked when this <see cref="IBehavior"/> has terminated.
        /// </summary>
        event Action<IBehavior, Behavior.Status> Terminated;

        /// <summary>
        /// The <see cref="BehaviourTree"/> this <see cref="IBehavior"/> is a part of.
        /// </summary>
        BehaviourTree Tree { get; }

        /// <summary>
        /// The current status.
        /// </summary>
        Behavior.Status CurrentStatus { get; }
        
        /// <summary>
        /// Whether the behaviour is terminated or not.
        /// </summary>
        bool IsTerminated { get; }

        /// <summary>
        /// Updates the behaviour.
        /// </summary>
        /// <returns>The status after the update.</returns>
        Behavior.Status Tick();

        /// <summary>
        /// Terminate the behavior.
        /// </summary>
        void Terminate();

        /// <summary>
        /// Terminate the behaviour with the given status.
        /// </summary>
        /// <param name="status">The status to terminate with.</param>
        void Terminate(Behavior.Status status);
        
        /// <summary>
        /// Starts this <see cref="IBehavior"/> as active behaviour in the <see cref="Tree"/>.
        /// </summary>
        void StartBehaviour();

        /// <summary>
        /// Starts this <see cref="IBehavior"/> as an active behaviour in the next frame.
        /// </summary>
        void StartNextFrame();

        /// <summary>
        /// Suspends the <see cref="IBehavior"/>.
        /// </summary>
        void Suspend();

        /// <summary>
        /// Continues this <see cref="IBehavior"/> if its <see cref="CurrentStatus"/> is <see cref="Behavior.Status.Suspended"/>.
        /// </summary>
        void Continue();
    }
}