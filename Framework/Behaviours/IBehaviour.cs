using System;

namespace Chinchillada.BehaviourSelections.BehaviourTree
{
    public interface IBehaviour
    {
        /// <summary>
        /// Event invoked when this <see cref="IBehaviour"/> has terminated.
        /// </summary>
        event Action<IBehaviour, Behaviour.Status> Terminated;

        /// <summary>
        /// The <see cref="BehaviourTree"/> this <see cref="IBehaviour"/> is a part of.
        /// </summary>
        BehaviourTree Tree { get; }

        /// <summary>
        /// The current status.
        /// </summary>
        Behaviour.Status CurrentStatus { get; }
        
        /// <summary>
        /// Whether the behaviour is terminated or not.
        /// </summary>
        bool IsTerminated { get; }

        /// <summary>
        /// Updates the behaviour.
        /// </summary>
        /// <returns>The status after the update.</returns>
        Behaviour.Status Tick();

        /// <summary>
        /// Terminate the behavior.
        /// </summary>
        void Terminate();

        /// <summary>
        /// Terminate the behaviour with the given status.
        /// </summary>
        /// <param name="status">The status to terminate with.</param>
        void Terminate(Behaviour.Status status);
        
        /// <summary>
        /// Starts this <see cref="IBehaviour"/> as active behaviour in the <see cref="Tree"/>.
        /// </summary>
        void StartBehaviour();

        /// <summary>
        /// Starts this <see cref="IBehaviour"/> as an active behaviour in the next frame.
        /// </summary>
        void StartNextFrame();

        /// <summary>
        /// Suspends the <see cref="IBehaviour"/>.
        /// </summary>
        void Suspend();

        /// <summary>
        /// Continues this <see cref="IBehaviour"/> if its <see cref="CurrentStatus"/> is <see cref="Behaviour.Status.Suspended"/>.
        /// </summary>
        void Continue();
    }
}