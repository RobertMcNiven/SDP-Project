///<summary>
///Functions for buttons on win screen.
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

/// <summary><c>Win</c>
/// Class to hold all the functions.
/// </summary>
public class Win : MonoBehaviour
{
    /// <summary><c>MainMenuButton</c>
    /// Function to bring user to main menu
    /// </summary>
    public void MainMenuButton()
    {
        Debug.Log("Main Menu");
        SceneManager.LoadScene("MainMenu");
    }
    /// <summary><c>QuitGame</c>
    /// Function to let the user quit the game
    /// </summary>
    public void QuitGame()
    {
        Debug.Log("Pressing Quit");
        Application.Quit();
    }
}
