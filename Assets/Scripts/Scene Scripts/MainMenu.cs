///<summary>
/// This is the script that will give the MainMenu scene's buttons all of
/// their functionality.
/// </summary>

// Main Menu operations as of now.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Need to use this to change scenes within uniity
using UnityEngine.SceneManagement;

///<summary> <c>MainMenu</c>
///These classes need to be public because we need to be able to access them when the button objects are clicked.
/// </summary>
public class MainMenu : MonoBehaviour
{
    ///<summary><c>PlayGame</c> Public function that is applied to the "PLAY" button object so that
    /// when clicked, it will play the main game loop.
    /// </summary> 
    public void PlayGame()
    {
        // Calling the LoadScene function from the SceneManager class
        SceneManager.LoadScene("Main");
    }

    ///<summary> <c>QuitGame</c> Public function that is applied to the "QUIT" button object so that
    /// when clicked, the application will close
    /// </summary>
    public void QuitGame()
    {
        Debug.Log("Quit");
        // Calling the Quit function from the Application class
        Application.Quit();
    }
}
