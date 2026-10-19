/************************************************************
 * COPYRIGHT:  2026
 * PROJECT:    PuzzleGame-Unity
 * FILE NAME:  TriggerZone.cs
 * DESCRIPTION: Puzzle system - runs actions when a tagged object enters an area.
 *
 * REVISION HISTORY:
 * Date [YYYY/MM/DD] | Author | Comments
 * ------------------------------------------------------------
 * 2026/10/18 | Your Name | Created class
 *
 ************************************************************/

using UnityEngine;
using UnityEngine.Events;

namespace CSG
{
    /// <summary>
    /// Runs actions when an object with the chosen tag enters this trigger.
    /// </summary>
    [RequireComponent(typeof(Collider))]
    public class TriggerZone : MonoBehaviour
    {
        #region Fields & Properties

        [Tooltip("Only objects with this tag will activate the zone.")]
        [SerializeField] private string _requiredTag = "Player";

        [Tooltip("Actions to run when a matching object enters.")]
        [SerializeField] private UnityEvent _onEnter;

        #endregion

        #region Trigger Events

        // Called by Unity when something enters this trigger collider.
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(_requiredTag))
            {
                _onEnter.Invoke();
            }//end if(other.CompareTag(_requiredTag))
        }//end OnTriggerEnter(Collider)

        #endregion
    }//end TriggerZone
}