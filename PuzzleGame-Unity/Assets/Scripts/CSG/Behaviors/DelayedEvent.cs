/************************************************************
 * COPYRIGHT:  2026
 * PROJECT:    PuzzleGame-Unity
 * FILE NAME:  DelayedEvent.cs
 * DESCRIPTION: Utility - runs a UnityEvent after a short delay.
 *
 * REVISION HISTORY:
 * Date [YYYY/MM/DD] | Author | Comments
 * ------------------------------------------------------------
 * 2026/10/19 | Elaina Martin | Created class
 *
 ************************************************************/

using UnityEngine;
using UnityEngine.Events;

namespace CSG
{
    /// <summary>
    /// Waits a set time, then runs whatever is hooked up to its event.
    /// </summary>
    public class DelayedEvent : MonoBehaviour
    {
        #region Fields & Properties

        [Tooltip("How long to wait before running the event, in seconds.")]
        [SerializeField] private float _delay = 0.2f;

        [Tooltip("What to run after the delay.")]
        [SerializeField] private UnityEvent _onDelayFinished;

        #endregion

        #region Feature Logic

        /// <summary>
        /// Starts the countdown. Call this from another event, like a trigger zone.
        /// </summary>
        public void StartDelay()
        {
            // Invoke calls a method by name after a delay, in seconds.
            Invoke(nameof(RunEvent), _delay);
        }//end StartDelay()

        // Runs the hooked-up event once the delay is over.
        private void RunEvent()
        {
            _onDelayFinished.Invoke();
        }//end RunEvent()

        #endregion
    }//end DelayedEvent
}