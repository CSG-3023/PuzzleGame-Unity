/************************************************************
 * COPYRIGHT:  2026
 * PROJECT:    PuzzleGame-Unity
 * FILE NAME:  SceneLoader.cs
 * DESCRIPTION: Scene system - restarts the current level.
 *
 * REVISION HISTORY:
 * Date [YYYY/MM/DD] | Author | Comments
 * ------------------------------------------------------------
 * 2000/10/18 | Your Name | Created class
 *
 ************************************************************/

using UnityEngine;
using UnityEngine.SceneManagement;

namespace CSG
{
    /// <summary>
    /// Loads scenes, such as restarting the current level or moving to another scene.
    /// </summary>
    public class SceneLoader : MonoBehaviour
    {
        #region Feature Logic

        /// <summary>
        /// Restarts the scene that is currently playing.
        /// </summary>
        public void ReloadCurrentScene()
        {
            string currentSceneName = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(currentSceneName);
        }//end ReloadCurrentScene()

        /// <summary>
        /// Loads a scene by its name.
        /// </summary>
        /// <param name="sceneName">The exact name of the scene to load.</param>
        public void LoadScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }//end LoadScene(string)

        #endregion
    }//end SceneLoader
}