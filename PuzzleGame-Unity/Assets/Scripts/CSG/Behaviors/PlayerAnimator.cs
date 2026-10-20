/************************************************************
 * COPYRIGHT:  2026
 * PROJECT:    PuzzleGame-Unity
 * FILE NAME:  PlayerAnimator.cs
 * DESCRIPTION: Visual system - switches the player's animations
 *              and flips the sprite based on movement.
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
    /// Tells the Animator when the player is moving and flips the sprite to face left or right.
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerAnimator : MonoBehaviour
    {
        #region Fields & Properties

        // Ignore tiny movements so the animation doesn't flicker.
        private const float MIN_MOVE_DISTANCE = 0.001f;

        // Turns the parameter name into a fast number once, instead of every frame.
        private static readonly int IS_MOVING = Animator.StringToHash("IsMoving");

        [Tooltip("The Animator on the archer sprite child.")]
        [SerializeField] private Animator _animator;

        [Tooltip("The SpriteRenderer on the archer sprite child.")]
        [SerializeField] private SpriteRenderer _spriteRenderer;

        private Rigidbody _rigidbody;

        // Where the player was last physics step.
        private Vector3 _lastPosition;
        
        [Tooltip("How long to keep facing the shot direction after firing, in seconds.")]
        [SerializeField] private float _facingLockTime = 0.7f;

        // Counts down while the facing is locked.
        private float _facingLockTimer;

        #endregion

        #region Initialization & Activation

        // Awake is called once on initialization before any Start methods.
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _lastPosition = _rigidbody.position;
        }//end Awake()

        #endregion

        #region Update Physics

        // FixedUpdate matches the physics steps the player moves on.
        private void FixedUpdate()
        {
            
            Vector3 moveDirection = _rigidbody.position - _lastPosition;
            moveDirection.y = 0f;
            _lastPosition = _rigidbody.position;

            bool isMoving = moveDirection.sqrMagnitude > MIN_MOVE_DISTANCE * MIN_MOVE_DISTANCE;
            _animator.SetBool(IS_MOVING, isMoving);

            // Count down the lock, and don't flip while it's active.
            if (_facingLockTimer > 0f)
            {
                _facingLockTimer -= Time.fixedDeltaTime;
            }
            else if (Mathf.Abs(moveDirection.x) > MIN_MOVE_DISTANCE)
            {
                _spriteRenderer.flipX = moveDirection.x < 0f;
            }//end if(facing locked or moving sideways)
        }//end FixedUpdate()
        
        /// <summary>
        /// Turns the archer to face left or right. Called by the firing pad.
        /// </summary>
        /// <param name="faceLeft">True to face left, false to face right.</param>
        public void SetFacingLeft(bool faceLeft)
        {
            _spriteRenderer.flipX = faceLeft;
            _facingLockTimer = _facingLockTime;
        }//end SetFacingLeft(bool)

        #endregion
    }//end PlayerAnimator
}