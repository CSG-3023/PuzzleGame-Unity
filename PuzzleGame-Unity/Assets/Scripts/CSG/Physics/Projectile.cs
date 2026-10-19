/************************************************************
 * COPYRIGHT:  2026
 * PROJECT:    PuzzleGame-Unity
 * FILE NAME:  Projectile.cs
 * DESCRIPTION: Puzzle system - removes the projectile when it hits an absorbing surface.
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
    /// Removes this projectile when it hits a surface marked with ProjectileAbsorber.
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    public class Projectile : MonoBehaviour
    {
        #region Collision Events

        // Called by Unity when this projectile hits something.
        private void OnCollisionEnter(Collision collision)
        {
            // collision.collider is the exact piece that was hit, even a child like BackPlate.
            if (collision.collider.GetComponent<ProjectileAbsorber>() != null)
            {
                Destroy(gameObject);
            }//end if
        }//end OnCollisionEnter(Collision)

        #endregion
    }//end Projectile
}