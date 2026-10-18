/************************************************************
* COPYRIGHT:  2026
* PROJECT:    PuzzleGame-Unity
* FILE NAME:  Launcher.cs
* DESCRIPTION: Puzzle system - fires projectiles for the bounce puzzle
*                    
* REVISION HISTORY:
* Date [YYYY/MM/DD] | Author | Comments
* ------------------------------------------------------------
* 2026/10/15 | Elaina Martin | Created class
*
************************************************************/

using UnityEngine;

namespace CSG
{

/// <summary>
/// Fires a projectile forward from a fire point.
/// </summary>
public class Launcher : MonoBehaviour
{
    #region Fields & Properties

    [Tooltip("The projectile prefab created when firing.")]
    [SerializeField] private Rigidbody _projectilePrefab;

    [Tooltip("Where the projectile appears and which way it flies.")]
    [SerializeField] private Transform _firePoint;

    [Tooltip("How fast the projectile travels, in units per second.")]
    [SerializeField] private float _launchSpeed = 6f;

    #endregion

    #region Feature Logic

    /// <summary>
    /// Creates a projectile at the fire point and sends it forward.
    /// </summary>
    [ContextMenu("Fire")]
    public void Fire()
    {
        // Stop if the launcher hasn't been set up in the Inspector.
        if (_projectilePrefab == null || _firePoint == null)
        {
            Debug.LogWarning($"{name} is missing a projectile prefab or fire point.");
            return;
        }

        Rigidbody projectile = Instantiate(_projectilePrefab, _firePoint.position, _firePoint.rotation);
        projectile.linearVelocity = _firePoint.forward * _launchSpeed;
    }//end Fire()
    
    #endregion
    }//end Launcher
}