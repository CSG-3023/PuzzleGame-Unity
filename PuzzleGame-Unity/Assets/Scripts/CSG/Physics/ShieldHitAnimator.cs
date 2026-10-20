/************************************************************
 * COPYRIGHT:  2026
 * PROJECT:    PuzzleGame-Unity
 * FILE NAME:  ShieldHitAnimator.cs
 * DESCRIPTION: Visuals - plays the knight's impact animation when an arrow hits the shield.
 *
 * REVISION HISTORY:
 * Date [YYYY/MM/DD] | Author | Comments
 * ------------------------------------------------------------
 * 2026/10/19 | Elaina Martin | Created class
 *
 ************************************************************/

using UnityEngine;

namespace CSG
{
    /// <summary>
    /// Plays the knight's impact animation when a projectile hits this shield.
    /// </summary>
    public class ShieldHitAnimator : MonoBehaviour
    {
        #region Fields

        [Tooltip("The Animator on the knight's sprite.")]
        [SerializeField] private Animator animator;

        #endregion

        #region Public Methods

        // Called by the Projectile when it bounces off this shield.
        public void PlayHit()
        {
            animator.SetTrigger("Hit");
        }//end PlayHit()

        #endregion
    }//end ShieldHitAnimator
}