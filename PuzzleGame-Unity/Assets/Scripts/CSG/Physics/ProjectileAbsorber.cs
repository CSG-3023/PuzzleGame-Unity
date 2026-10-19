/************************************************************
 * COPYRIGHT:  2026
 * PROJECT:    PuzzleGame-Unity
 * FILE NAME:  ProjectileAbsorber.cs
 * DESCRIPTION: Puzzle system - removes projectiles that hit non-reflective surfaces.
 *
 * REVISION HISTORY:
 * Date [YYYY/MM/DD] | Author | Comments
 * ------------------------------------------------------------
 * 2026/10/18 | Elaina Martin | Created class
 *
 ************************************************************/

using UnityEngine;

namespace CSG
{
    /// <summary>
    /// Destroys any projectile that touches this object.
    /// </summary>
    public class ProjectileAbsorber : MonoBehaviour
    {
        #region Fields & Properties

        private const string PROJECTILE_TAG = "Projectile";

        #endregion

        #region Collision Events

        // Called by Unity when something collides with this object.
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag(PROJECTILE_TAG))
            {
                Destroy(collision.gameObject);
            }
        }//end OnCollisionEnter(Collision)

        #endregion
    }//end ProjectileAbsorber
}