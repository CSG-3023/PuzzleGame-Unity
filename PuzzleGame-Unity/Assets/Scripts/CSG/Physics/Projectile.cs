/************************************************************
 * COPYRIGHT:  2026
 * PROJECT:    PuzzleGame-Unity
 * FILE NAME:  Projectile.cs
 * DESCRIPTION: Puzzle system - removes the projectile when it hits an absorbing surface
 *              and tells shields when they've been hit.
 *
 * REVISION HISTORY:
 * Date [YYYY/MM/DD] | Author | Comments
 * ------------------------------------------------------------
 * 2026/10/18 | Elaina Martin | Created class
 * 2026/10/19 | Elaina Martin | Added shield hit animation call
 *
 ************************************************************/

using UnityEngine;

namespace CSG
{
    /// <summary>
    /// Removes this projectile when it hits a surface marked with ProjectileAbsorber,
    /// and plays the shield's hit animation when it bounces off a shield.
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    public class Projectile : MonoBehaviour
    {
        #region Collision Events

        // Called by Unity when this projectile hits something.
        private void OnCollisionEnter(Collision collision)
        {
            // If we hit a shield, tell it to play its hit animation.
            ShieldHitAnimator shield = collision.collider.GetComponent<ShieldHitAnimator>();
            if (shield != null)
            {
                shield.PlayHit();
            }//end if

            // collision.collider is the exact piece that was hit, even a child like BackPlate.
            if (collision.collider.GetComponent<ProjectileAbsorber>() != null)
            {
                Destroy(gameObject);
            }//end if
        }//end OnCollisionEnter(Collision)

        #endregion
    }//end Projectile
}