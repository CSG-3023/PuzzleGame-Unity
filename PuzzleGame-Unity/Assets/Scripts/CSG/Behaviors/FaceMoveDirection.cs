/************************************************************
 * COPYRIGHT:  2026
 * PROJECT:    PuzzleGame-Unity
 * FILE NAME:  FaceMoveDirection.cs
 * DESCRIPTION: Visual system - turns a child sprite to face the
 *              direction its object is moving.
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
    /// Rotates a visual child so it always points the way this object is moving.
    /// </summary>
    public class FaceMoveDirection : MonoBehaviour
    {
        #region Fields & Properties

        // Ignore tiny movements so the sprite doesn't jitter.
        private const float MIN_MOVE_DISTANCE = 0.001f;

        [Tooltip("The child object to rotate (the arrow sprite).")]
        [SerializeField] private Transform _visual;

        [Tooltip("Extra turn in degrees, in case the sprite points the wrong way.")]
        [SerializeField] private float _angleOffset = 0f;

        // Where this object was last frame.
        private Vector3 _lastPosition;

        // The sprite's starting rotation (keeps it lying flat).
        private Quaternion _startRotation;

        #endregion

        #region Initialization & Activation

        // Awake is called once on initialization before any Start methods.
        private void Awake()
        {
            _lastPosition = transform.position;
            _startRotation = _visual.rotation;
        }//end Awake()

        #endregion

        #region Update

        // LateUpdate runs after all movement for this frame is done.
        private void LateUpdate()
        {
            Vector3 moveDirection = transform.position - _lastPosition;
            moveDirection.y = 0f;
            _lastPosition = transform.position;

            if (moveDirection.sqrMagnitude < MIN_MOVE_DISTANCE * MIN_MOVE_DISTANCE)
            {
                return;
            }//end if(too small to count)

            // Turn the direction into an angle. Minus 90 because the
            // arrow already points right (+X) when its angle is 0.
            float angle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg - 90f;

            _visual.rotation = Quaternion.Euler(0f, angle + _angleOffset, 0f) * _startRotation;
        }//end LateUpdate()

        #endregion
    }//end FaceMoveDirection
}