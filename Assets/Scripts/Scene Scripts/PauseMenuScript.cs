/// <summary> Followed a tutorial for this one mainly for the look
/// https://www.youtube.com/watch?v=JivuXdrIHK0
/// </summary> 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

/// <summary> <c>PauseMenuScript</c>
/// Script to give the pause menu functionality.
/// </summary>
public class PauseMenuScript : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject PauseMenuUI;

    /// <summary> <c>Update</c>
    /// Update function that Unity ooks for to call every frame.
    /// </summary>
    void Update()
    {
        // Had trouble with this because we are using a different input system than default, had to make sure we were on mouse and keyeboard.
        if (Keyboard.current.escapeKey.wasPressedThisFrame) // if the escape key is pressed on the keyboard
        {
            if (GameIsPaused) // If the game is paused GameIsPaused will be true from the Pause() function.
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    // Functions for the different button presses

    /// <summary> <c>Resume</c>
    /// Resume button function called on click
    /// </summary>
    public void Resume()
    {
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }
    /// <summary> <c>Pause</c>
    /// Pause button function called on clck
    /// </summary>
    void Pause()
    {
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }
    /// <summary> <c>LoadMenu</c>
    /// On click of the menu button that will bring the player to the main menu.
    /// </summary>
    public void LoadMenu()
    {
        Debug.Log("Load Menu...");
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
    /// <summary> <c>QuitGame</c>
    /// On click of the quit button that will quit the game entirely.
    /// </summary>
    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
