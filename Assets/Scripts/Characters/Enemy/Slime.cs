using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A simple enemy that will move towards the player for a second, then stop moving, then move again. 
/// This is to simulate it 'jumping' towards the player.
/// </summary>
public class Slime : Enemy
{
    /// <summary>
    /// A state for when the Slime is Idle and does not have a specific 'goal' in terms of gameplay.
    /// It enters this state between 'jumps'
    /// </summary>
    public class SlimeFSMState_Idle : EnemyFSMState_Idle
    {
        /// <summary>
        /// Constructor that sets this State's enum ID to IDLE. This is so the FSM can see what key it possesses.
        /// </summary>
        /// <param name="enemy"> The enemy object which must be passed to the base constructor so that it may reference the enemy's fields</param>
        public SlimeFSMState_Idle(Enemy enemy) : base(enemy)
        {
            _id = EnemyFSMStateType.IDLE;
        }

        /// <summary>
        /// While in the state, this gets called every frame.
        /// This function times how long the Slime has been Idle. Once it has been Idle for the specified moveDelay, it will switch back to the Movement State
        /// </summary>
        public override void Update()
        {
            base.Update();

            thisEnemy.timer += Time.deltaTime;          // each frame, add how much time has passed.

            if (thisEnemy.timer >= thisEnemy.MoveDelay)     // and they have been delayed long enough
            {
                thisEnemy.timer = 0.0f;           // reset the timer
                thisEnemy.enemyFSM.SetCurrentState(EnemyFSMStateType.MOVEMENT);        // set them to start moving
                thisEnemy.IsMoving = true;
            }
        }
    }

    /// <summary>
    /// A state for when the Slime is Idle and does not have a specific 'goal' in terms of gameplay.
    /// It enters this state during 'jumps'
    /// </summary>
    public class SlimeFSMState_Movement : EnemyFSMState_Movement
    {
        /// <summary>
        /// Constructor that sets this State's enum ID to MOVEMENT. This is so the FSM can see what key it possesses.
        /// </summary>
        /// <param name="enemy"> The enemy object which must be passed to the base constructor so that it may reference the enemy's fields</param>
        public SlimeFSMState_Movement(Enemy enemy) : base(enemy)
        {
            _id = EnemyFSMStateType.MOVEMENT;
        }

        /// <summary>
        /// While in the state, this gets called every frame.
        /// This function times how long the Slime has been moving. Once it has been Idle for the specified moveLength, it will switch back to the Idle State.
        /// (Moving the slime is not done in this function, but in the base FixedUpdate function of EnemyFSMState_Movement)
        /// </summary>
        public override void Update()
        {
            base.Update();

            thisEnemy.timer += Time.deltaTime;        // each frame, add how much time has passed.

            if (thisEnemy.timer >= thisEnemy.MoveLength)    // and if they have been moving for long enough
            {
                thisEnemy.timer = 0.0f;           // reset the timer
                thisEnemy.enemyFSM.SetCurrentState(EnemyFSMStateType.IDLE);        // set them to stop moving
                thisEnemy.IsMoving = false;
            }
        }
    }

    /// <summary>
    /// A function that adds a set of States to the dictionary. 
    /// The Slime has its own unique Idle and Movement states added to the dictionary.
    /// It uses the base Enemy Take Damage and Dead states.
    /// By default, the Slime is in the Idle state.
    /// </summary>
    protected override void MakeFSMDictionary()
    {
        // add all the enemy states to the FSM dictionary
        enemyFSM.Add(new SlimeFSMState_Idle(this));
        enemyFSM.Add(new SlimeFSMState_Movement(this));
        enemyFSM.Add(new EnemyFSMState_TakeDamage(this));
        enemyFSM.Add(new EnemyFSMState_Dead(this));

        // set the state to idle by default
        enemyFSM.SetCurrentState(EnemyFSMStateType.IDLE);
    }

    /// <summary>
    /// This function gets called every frame. Used for non-physics related functions.
    /// The Slime's update will call the base Update while Idle, but while it is moving it will skip to just calling the enemyFSM.Update().
    /// This is so it cannot change directions in the middle of its jump, which is done in the base Update function.
    /// </summary>
    protected override void Update()
    {
        if (isMoving)
            enemyFSM.Update();             // only let the slime change directions while it is getting ready to jump
        else
            base.Update();                 // this update includes the targetPlayer function
    }
}
