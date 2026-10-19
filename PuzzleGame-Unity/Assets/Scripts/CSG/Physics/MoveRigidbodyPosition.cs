/************************************************************
 * COPYRIGHT:  2026
 * PROJECT:    PuzzleGame-Unity
 * FILE NAME:  MoveRigidbodyPosition.cs
 * DESCRIPTION: Movement system - moves an object using its Rigidbody.
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
    /// Moves an object with Rigidbody.MovePosition so it still collides with walls and objects.
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    public class MoveRigidbodyPosition : MonoBehaviour
    {
        #region Fields & Properties

        // The Rigidbody this component moves.
        private Rigidbody _rigidbody;

        #endregion

        #region Initialization & Activation

        // Awake is called once on initialization before any Start methods.
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }//end Awake()

        #endregion

        #region Feature Logic

        /// <summary>
        /// Moves the Rigidbody by the given velocity for one physics step.
        /// </summary>
        /// <param name="velocity">The direction and speed to move, in units per second.</param>
        public void Move(Vector3 velocity)
        {
            // Velocity is "per second," so scale it down to one physics step.
            Vector3 displacement = velocity * Time.fixedDeltaTime;
            _rigidbody.MovePosition(_rigidbody.position + displacement);
        }//end Move(Vector3)

        #endregion
    }//end MoveRigidbodyPosition
}