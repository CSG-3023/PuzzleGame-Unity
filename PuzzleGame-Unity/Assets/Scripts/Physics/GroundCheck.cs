/************************************************************
 * COPYRIGHT:  2026
 * PROJECT:    Sandbox
 * FILE NAME:  GroundCheck.cs
 * DESCRIPTION: Evaluates whether an object is currently touching a valid ground surface.
 *
 * REVISION HISTORY:
 * Date [YYYY/MM/DD] | Author | Comments
 * ------------------------------------------------------------
 * 2026/10/08 | Akram Taghavi-Burris | Created class
 *
 ************************************************************/

using UnityEngine;

namespace CSG.Physics
{
    /// <summary>
    /// Checks for valid ground contact within a specified radius or distance.
    /// Useful for kinematic player movement, jumping, and landing checks.
    /// </summary>
    public class GroundCheck : MonoBehaviour
    {
        #region Fields & Properties

        [Header("Check Configuration")]
        [SerializeField]
        [Tooltip("Distance and direction from this object to check for ground.")]
        private Vector3 _checkOffset = new Vector3(0f, -0.5f, 0f);

        [SerializeField]
        [Tooltip("Radius of the overlap sphere used to detect ground surface colliders.")]
        private float _checkRadius = 0.2f;

        [SerializeField]
        [Tooltip("Layers recognized as valid ground surfaces.")]
        private LayerMask _groundLayers = ~0;

        [Header("Debug Visualization")]
        [SerializeField]
        [Tooltip("Color of the gizmo sphere displayed in the Scene view.")]
        private Color _gizmoColor = Color.green;

        /// <summary>
        /// Gets whether the object is currently grounded based on the latest check.
        /// </summary>
        public bool IsGrounded { get; private set; }

        #endregion

        #region Update Physics

        // FixedUpdate is called at a fixed interval for physics calculations.
        private void FixedUpdate()
        {
            // Perform the ground check 
            IsGrounded = CheckIsGrounded();
            
           // Debug.Log($"{gameObject.name} IsGrounded =  {IsGrounded}");

        } //end Update()

        #endregion

        #region Feature Logic

        /// <summary>
        /// Performs an overlap sphere check to detect if valid ground colliders are present.
        /// </summary>
        /// <returns>True if ground colliders are detected; false otherwise.</returns>
        public bool CheckIsGrounded()
        {
            // Calculate the world position of the ground check center point
            Vector3 checkPosition = transform.position + _checkOffset;

            // Check for any colliders overlapping the sphere within the ground layer mask
            return UnityEngine.Physics.CheckSphere(checkPosition, _checkRadius, _groundLayers.value);

        } //end CheckIsGrounded()

        #endregion

        #region Editor Visualization

        // OnDrawGizmosSelected draws visual debugging aids in the Scene view when selected
        private void OnDrawGizmosSelected()
        {
            // Set gizmo color for scene visualization
            Gizmos.color = _gizmoColor;

            // Draw a wireframe sphere showing the ground check area
            Vector3 checkPosition = transform.position + _checkOffset;
            Gizmos.DrawWireSphere(checkPosition, _checkRadius);

        } //end OnDrawGizmosSelected()

        #endregion

    } //end GroundCheck

} //end namespace CSG.Physics