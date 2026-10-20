/************************************************************
 * COPYRIGHT:  2026
 * PROJECT:    Sandbox
 * FILE NAME:  PlayerController.cs
 * DESCRIPTION: Handles player input via the New Input System and drives
 *              movement and jumping
 *
 * REVISION HISTORY:
 * Date [YYYY/MM/DD] | Author | Comments
 * ------------------------------------------------------------
 * 2026/10/10 | Akram Taghavi-Burris | Created class
 * 2026/10/18 | Elaina Martin | Added movement component
 ************************************************************/

using UnityEngine;
using UnityEngine.InputSystem;
using CSG.Physics;
    
namespace CSG.Controller
{
    /// <summary>
    /// Controls character movement, jumping, and gravity using Unity's New Input System
    /// </summary>
    [RequireComponent(typeof(GroundCheck), typeof(MoveRigidbodyPosition))]
    public class PlayerController : MonoBehaviour
    {
        #region Fields & Properties

        [Header("Movement Settings")]
        [SerializeField]
        [Tooltip("Horizontal movement speed of the player.")]
        private float _walkSpeed = 5f;

        [Header("Jump & Gravity Settings")]
        [SerializeField]
        [Tooltip("Initial upward speed applied when jumping.")]
        private float _jumpForce = 7f;

        [SerializeField]
        [Tooltip("Downward acceleration applied when in the air.")]
        private float _gravity = 15f;
        
        // Component references
        //Declare field for movement component
        private MoveRigidbodyPosition _moveRigidbodyPosition;
        private GroundCheck _groundCheck;
        
        // Movement state tracking
        private Vector2 _rawInputVector;
        private float _verticalVelocity;
        private bool _jumpRequested;

        #endregion

        #region Initialization & Activation

        // Awake is called once on initialization before any Start methods
        private void Awake()
        {
            // Cache required component references
            /* Try and get movement component */
            _moveRigidbodyPosition = GetComponent<MoveRigidbodyPosition>();
            _groundCheck = GetComponent<GroundCheck>();

        } //end Awake()

        #endregion

        #region Update Physics

        // FixedUpdate is called at fixed intervals for physics calculations
        private void FixedUpdate()
        {
            // Process vertical velocity, gravity, and jumping
            UpdateVerticalVelocity();

            // Calculate full 3D direction vector
            Vector3 movementVector = CalculateMovementVector();

            // Apply movement through MoveRigidbodyPosition
            ApplyMovement(movementVector);

        } //end FixedUpdate()

        #endregion

        #region Input System Callbacks

        /// <summary>
        /// Input System Callback: Called when the Move action is performed, started, or canceled.
        /// </summary>
        /// <param name="context">Input action context containing Vector2 movement data.</param>
        public void OnMove(InputAction.CallbackContext context)
        {
           
            // Read the Vector2 value from WASD / Thumbstick
            _rawInputVector = context.ReadValue<Vector2>();
            
            Debug.Log($"OnMove, move in the direction : {_rawInputVector}");

        } //end OnMove()

        /// <summary>
        /// Input System Callback: Called when the Jump action is performed.
        /// </summary>
        /// <param name="context">Input action context containing button state data.</param>
        public void OnJump(InputAction.CallbackContext context)
        {
            Debug.Log("OnJump");
            
            // Only register jump on the initial button press (performed state)
            if (context.performed && _groundCheck.IsGrounded)
            {
                _jumpRequested = true;

            } //end if(context.performed && _groundCheck.IsGrounded)

        } //end OnJump()

        #endregion

        #region Feature Logic

        /// <summary>
        /// Updates the Y-axis velocity applying gravity and executing jumps.
        /// </summary>
        private void UpdateVerticalVelocity()
        {
            // Reset vertical velocity when grounded to maintain firm contact with floors/platforms
            if (_groundCheck.IsGrounded && _verticalVelocity < 0f)
            {
                _verticalVelocity = -1f;

            } //end if(_groundCheck.IsGrounded...)

            // Process Jump request
            if (_jumpRequested)
            {
                _verticalVelocity = _jumpForce;
                _jumpRequested = false;

            } //end if(_jumpRequested)

            // Apply downward gravity force over fixed delta time
            _verticalVelocity -= _gravity * Time.fixedDeltaTime;

        } //end UpdateVerticalVelocity()

        /// <summary>
        /// Combines raw input vector X/Z values with vertical Y velocity.
        /// </summary>
        /// <returns>Combined 3D movement direction vector.</returns>
        private Vector3 CalculateMovementVector()
        {
            // Map 2D input (X, Y) to 3D world space (X, Y, Z)
            Vector3 horizontal = new Vector3(_rawInputVector.x, 0f, _rawInputVector.y) * _walkSpeed;
            Vector3 vertical = Vector3.up * _verticalVelocity;

            // Top-down puzzle: no jumping or falling, so ignore vertical movement.
            return horizontal;

        } //end CalculateMovementVector()

        /// <summary>
        /// Passes direction and magnitude to the MoveRigidbodyPosition component.
        /// </summary>
        /// <param name="movementVector">The target 3D velocity vector.</param>
        private void ApplyMovement(Vector3 movementVector)
        {   
            //send values to move component
            if (_moveRigidbodyPosition != null)
            {
                _moveRigidbodyPosition.Move(movementVector);
            }//end if(_moveRigidbodyPosition != null)
            

        } //end ApplyMovement()

        #endregion

    } //end PlayerController

} //end namespace CSG.Controller