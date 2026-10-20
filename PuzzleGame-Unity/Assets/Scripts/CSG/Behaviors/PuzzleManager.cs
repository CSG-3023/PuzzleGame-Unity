/************************************************************
 * COPYRIGHT:  2026
 * PROJECT:    PuzzleGame-Unity
 * FILE NAME:  PuzzleManager.cs
 * DESCRIPTION: Puzzle system - checks when all targets are hit and finishes the puzzle.
 *
 * REVISION HISTORY:
 * Date [YYYY/MM/DD] | Author | Comments
 * ------------------------------------------------------------
 * 2026/10/18 | Elaina Martin | Created class
 *
 ************************************************************/

using UnityEngine;
using UnityEngine.Events;

namespace CSG
{
    /// <summary>
    /// Watches the puzzle's targets and runs the solved actions once all of them are hit.
    /// </summary>
    public class PuzzleManager : MonoBehaviour
    {
        #region Fields & Properties

        [Tooltip("All targets that must be hit to solve the puzzle.")]
        [SerializeField] private Target[] _targets;

        [Tooltip("Seconds to wait after solving, so the player can see the hit.")]
        [SerializeField] private float _solvedDelay = 1f;

        [Tooltip("Actions to run when the puzzle is solved.")]
        [SerializeField] private UnityEvent _onPuzzleSolved;

        /// <summary>True once every target has been hit.</summary>
        public bool IsSolved { get; private set; }

        #endregion

        #region Feature Logic

        /// <summary>
        /// Checks whether every target has been hit, and finishes the puzzle if so.
        /// </summary>
        public void CheckPuzzle()
        {
            if (IsSolved)
            {
                return;
            }//end if(IsSolved)

            // If any target isn't hit yet, the puzzle isn't solved.
            foreach (Target target in _targets)
            {
                if (target == null || !target.IsHit)
                {
                    return;
                }//end if
            }//end foreach

            IsSolved = true;
            Invoke(nameof(FinishPuzzle), _solvedDelay);
        }//end CheckPuzzle()

        #endregion

        #region Helpers

        // Runs the solved actions, like loading the End Scene.
        private void FinishPuzzle()
        {
            _onPuzzleSolved.Invoke();
        }//end FinishPuzzle()

        #endregion
    }//end PuzzleManager
}