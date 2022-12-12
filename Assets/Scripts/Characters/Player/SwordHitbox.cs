/// <summary>
/// SwordHitbox.cs
/// 
/// A class that checks to see if the colliding enemy's Collider2D is tagged enemy
/// 
/// A very gung-ho implementation of the player's sword hitbox
/// no sources, glhf finding some lol
/// everything is pretty much conceptual
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The hitbox on the Player's sword uses this.
/// Possibility to make it generic.
/// </summary>
public class SwordHitbox : MonoBehaviour
{
    public Rigidbody2D hitbox;
    public BoxCollider2D col;
    public Collision2D enemyCol;
    public ContactFilter2D conFilter;
    
    /// <summary>
    /// <c>Start</c>Start is called at object creation.
    /// </summary>
    void Start()
    {
        hitbox = GetComponent<Rigidbody2D>();
        col = GetComponent<BoxCollider2D>();

        Debug.Log("Hitbox Start!");
        col.enabled = false;

        Debug.Log(conFilter.isFiltering);
    }

    /// <summary>
    /// <c>Awake</c>Awake is called when object is enabled
    /// </summary>
    void Awake()
    {
        Debug.Log("Hitbox Awake!");
    }

    /// <summary>
    /// <c>Awake</c>Update is called 200 times a frame
    /// </summary>
    void Update()
    {

    }

    /// <summary>
    /// <c>OnTriggerEnter2D</c>Unity's physics system
    /// </summary>
    /// <param name="other">the colloider we're checking</param>
    void OnTriggerEnter2D(Collider2D other)
    {
        if (col.IsTouching(col, conFilter))
        {
            var entity = GetComponent<Enemy>();
            int damage = entity.HealthReduce(20);
            Debug.Log("Enemy hit!");
        }
        Debug.Log("Sword Attack Swing!");
    }
}
