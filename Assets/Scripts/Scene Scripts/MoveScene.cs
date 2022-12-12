using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Need to use this to change scenes within uniity
using UnityEngine.SceneManagement;

/// <summary>
/// Simple script to move the player to a specified scene as they enter a 'door'
/// </summary>
public class MoveScene : MonoBehaviour
{
    [SerializeField]
    private string scene;       // name of the scene to transition too

    /// <summary>
    /// Checks for collision with other game objects and runs when it detects one.
    /// When it runs, if a Player collided with it, the game transitions to the specified scene.
    /// </summary>
    /// <param name="col"> The specific instance of collision. Automatically passed by Unity </param>
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player") //   if it was a player that collided with the object
        {
            // Calling the LoadScene function from the SceneManager class. Transition to specified scene.
            SceneManager.LoadScene(scene);
        }
    }
}
