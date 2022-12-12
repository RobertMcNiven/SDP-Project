using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script is to be used for a simple 'enemy' which bounces back and forth. 
/// It is less of an enemy and more of an obstacle for the player.
/// </summary>
public class SpinningSpikeTrap : Enemy
{
    protected Vector2 right;
    protected bool startsMovingRight = true;

    /// <summary>
    /// This function reverses the direction the object is moving when it collides with something.
    /// The GameObject this is attached to is set to only collide with walls and the player. 
    /// </summary>
    /// <param name="col"></param>
    protected override void OnCollisionEnter2D(Collision2D col)  // if they hit something
    {
        direction *= -1;        // reverse the direction
    }

    /// <summary>
    /// Essentially Unity's Constructor. Gets called upon creation of the GameObject.
    /// Calls Enemy's constructor, initializes the objects movement vector, 
    /// sets it to move right or left depending on the startMovingRight bool, and 
    /// sets the objects FSM state to MOVEMENT so that it will use Enity's MOVEMENT functions.
    /// </summary>
    protected override void Awake()
    {
        base.Awake();

        // initialize vectors
        right = new Vector2(1f, 0f);

        if (startsMovingRight)
            this.direction = right;         // set it to move right
        else
            this.direction = right * -1;     // set it to move left

        enemyFSM.SetCurrentState(EnemyFSMStateType.MOVEMENT);           //set it to move
    }

    /// <summary>
    /// Update gets called on the object every frame.
    /// Instead of using Enemy's base update, we call enemyFSM's update (an object within Enemy).
    /// This lets us skip the 'targeting' sequence of Enemy's update where it changes the direction 
    /// the object is moving to be towards the player.
    /// enemyFSM.Update() then sends us to the Update function of the FSM State the object currently has.
    /// </summary>
    protected override void Update()
    {
        body.AddTorque(10 * Time.deltaTime);
        enemyFSM.Update();              // dont allow it to change directions
    }


}
