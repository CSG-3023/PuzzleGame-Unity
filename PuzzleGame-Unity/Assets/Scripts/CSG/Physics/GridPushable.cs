/************************************************************
* COPYRIGHT:  2026
* PROJECT:    PuzzleGame-Unity
* FILE NAME:  GridPushable.cs
* DESCRIPTION: Puzzle system - lets objects be pushed one grid tile at a time.
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
    /// Lets an object be pushed exactly one grid tile when a chosen object bumps into it.
    /// </summary>
    public class GridPushable : MonoBehaviour
    {
        #region Fields & Properties

        // Size of one grid tile, in units.
        private const float TILE_SIZE = 1f;

        // Half the size of the box used to check if the next tile is empty.
        private const float CHECK_HALF_SIZE = 0.4f;

        // Height of the check box, so it doesn't detect the floor.
        private const float CHECK_HEIGHT = 0.5f;

        [Tooltip("Only objects with this tag can push this object.")]
        [SerializeField] private string _pusherTag = "Player";
        
        [Tooltip("How long the pusher must keep pushing before the object moves, in seconds.")]
        [SerializeField] private float _pushHoldTime = 0.25f;

        // How long the pusher has been pushing so far.
        private float _pushTimer;
        #endregion

        #region Collision Events

        // Called by Unity every physics step while something is touching this object.
        private void OnCollisionStay(Collision collision)
        {
            if (!collision.gameObject.CompareTag(_pusherTag))
            {
                return;
            }//end if(!collision.gameObject.CompareTag(_pusherTag))

            // Count up while the pusher keeps leaning on this object.
            _pushTimer += Time.fixedDeltaTime;

            if (_pushTimer < _pushHoldTime)
            {
                return;
            }//end if(_pushTimer < _pushHoldTime)

            _pushTimer = 0f;
            Vector3 pushDirection = GetPushDirection(collision.transform.position);
            TryMove(pushDirection);
        }//end OnCollisionStay(Collision)

        // Called by Unity when something stops touching this object.
        private void OnCollisionExit(Collision collision)
        {
            if (collision.gameObject.CompareTag(_pusherTag))
            {
                _pushTimer = 0f;
            }//end if(collision.gameObject.CompareTag(_pusherTag))
        }//end OnCollisionExit(Collision)

        #endregion

        #region Feature Logic

        /// <summary>
        /// Moves one tile in the given direction if that tile is empty.
        /// </summary>
        /// <param name="direction">The straight grid direction to move.</param>
        public void TryMove(Vector3 direction)
        {
            Vector3 targetPosition = transform.position + direction * TILE_SIZE;

            if (IsTileBlocked(targetPosition))
            {
                return;
            }//end if(IsTileBlocked(targetPosition))

            transform.position = targetPosition;
        }//end TryMove(Vector3)

        #endregion

        #region Helpers

        // Turns the pusher's position into one of four straight directions.
        private Vector3 GetPushDirection(Vector3 pusherPosition)
        {
            Vector3 offset = transform.position - pusherPosition;

            // Push along whichever axis the pusher is more lined up with.
            if (Mathf.Abs(offset.x) > Mathf.Abs(offset.z))
            {
                return new Vector3(Mathf.Sign(offset.x), 0f, 0f);
            }//end if

            return new Vector3(0f, 0f, Mathf.Sign(offset.z));
        }//end GetPushDirection(Vector3)

        // Checks whether something solid is already on the target tile.
        private bool IsTileBlocked(Vector3 targetPosition)
        {
            Vector3 checkCenter = new Vector3(targetPosition.x, CHECK_HEIGHT, targetPosition.z);
            Vector3 halfSize = Vector3.one * CHECK_HALF_SIZE;

            // "UnityEngine." is needed because CSG has its own Physics namespace.
            return UnityEngine.Physics.CheckBox(checkCenter, halfSize, Quaternion.identity,
                UnityEngine.Physics.AllLayers, QueryTriggerInteraction.Ignore);
        }//end IsTileBlocked(Vector3)

        #endregion
    }//end GridPushable
}