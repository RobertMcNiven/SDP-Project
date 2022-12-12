/// <summary>
/// This is for the healthbar movement, the set health is in the entity class.
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary> <c>HealthBar</c>
/// Healthbar class to apply to the slider object
/// </summary>
public class HealthBar : MonoBehaviour
{
    //Creating a slider object that I can change absed on the current health
    // These fields need to be public so that all the classes can acess them
    // because there will be things outside of player/enemy actions that will
    // affect health.
    public Slider slider;
    public Player m_player;

    /// <summary> <c>Update</c>
    /// This is called every fram of the game.
    /// </summary>
    void Update()
    {
        SetHealth(m_player);
    }
    /// <summary> <c>SetMaxHealth</c>
    /// Method to initialize the max health of the entity and set the slider to that health
    /// </summary>
    /// <param name="player"></param>
    public void SetMaxHealth(Player player)
    {
        slider.maxValue = player.HealthTotal;
        slider.value = player.CurrentHealth;
    }
    /// <summary> <c>SetHealth</c>
    /// Changes the slider based on the health of the player
    /// </summary>
    /// <param name="player"></param>
    public void SetHealth(Player player)
    {
        slider.value = player.CurrentHealth;
    }

}
