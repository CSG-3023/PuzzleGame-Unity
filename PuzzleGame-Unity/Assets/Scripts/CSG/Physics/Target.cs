/************************************************************
 * COPYRIGHT:  2026
 * PROJECT:    PuzzleGame-Unity
 * FILE NAME:  Target.cs
 * DESCRIPTION: Puzzle system - detects when a projectile hits a target.
 *
 * REVISION HISTORY:
 * Date [YYYY/MM/DD] | Author | Comments
 * ------------------------------------------------------------
 * 2026/10/18 | Elaina Martin | Created class
 *
 ************************************************************/

using UnityEngine;
using UnityEngine.Events;

namespace CSG
{
    /// <summary>
    /// Detects when a projectile hits it, removes the projectile, and announces the hit.
    /// </summary>
    public class Target : MonoBehaviour
    {
        #region Fields & Properties

        private const string PROJECTILE_TAG = "Projectile";

        [Tooltip("Actions to run when a projectile hits this target.")]
        [SerializeField] private UnityEvent _onHit;

        /// <summary>True once a projectile has hit this target.</summary>
        public bool IsHit { get; private set; }

        #endregion

        #region Collision Events

        // Called by Unity when something collides with this object.
        private void OnCollisionEnter(Collision collision)
        {
            // Ignore anything that isn't a projectile, and ignore repeat hits.
            if (IsHit || !collision.gameObject.CompareTag(PROJECTILE_TAG))
            {
                return;
            }

            IsHit = true;
            Destroy(collision.gameObject);
            _onHit.Invoke();
        }//end OnCollisionEnter(Collision)

        #endregion
    }//end Target
}