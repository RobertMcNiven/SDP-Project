///<summary>
/// Knight class
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary><c>Knight</c>
/// Class Knight that extends the Player. A knight is a type of player.
/// </summary>
public class KnightClass : Player
{
    /// <summary>
    /// Healthbar for the Knight
    /// </summary>
    public HealthBar healthbar;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Knight has been created!");
    }
}
