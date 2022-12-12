/// <summary>
/// Making a new type of enemy derived from the enemy class.
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> <c>Masked_Enemy</c>
/// Masked_Enemy is derived from the Emeny class.
/// </summary>
public class Masked_Enemy : Enemy
{
    /// <summary> <c>Masked_EnemyFSMState_Idle</c>
    /// Making a new FSM idle class derived from the EnemyFSM idle
    /// </summary>
    public class Masked_EnemyFSMState_Idle : EnemyFSMState_Idle
    {
        /// <summary> <c>Masked_EnemyFSMState_Idle</c>
        /// Setting IDLE for this enemy
        /// </summary>
        /// <param name="enemy"></param>
        public Masked_EnemyFSMState_Idle(Enemy enemy) : base(enemy)
        {
            _id = EnemyFSMStateType.IDLE;
        }
        /// <summary> <c>Update</c>
        /// Overriding the Update in EnemyFSMState_Idle so this is called with this enemies specific traits.
        /// </summary>
        public override void Update() // Overloading
        {
            base.Update();

            thisEnemy.timer += Time.deltaTime;

            if (thisEnemy.timer >= thisEnemy.MoveDelay)     // and they have been delayed long enough
            {
                thisEnemy.timer = 0.0f;           // reset the timer
                thisEnemy.enemyFSM.SetCurrentState(EnemyFSMStateType.MOVEMENT);        // set them to start moving
                thisEnemy.IsMoving = true;
            }
        }
    }
    /// <summary> <c>Masked_EnemyFSMState_Movement</c>
    /// Making a new FSM Movement class derived from the EnemyFSM Movement
    /// </summary>
    public class Masked_EnemyFSMState_Movement : EnemyFSMState_Movement
    {
        /// <summary> <c>Masked_EnemyFSMState_Movement</c>
        /// Setting movement ID for FSM dictionary
        /// </summary>
        /// <param name="enemy"></param>
        public Masked_EnemyFSMState_Movement(Enemy enemy) : base(enemy)
        {
            _id = EnemyFSMStateType.MOVEMENT;
        }
        /// <summary> <c>Update</c>
        /// Overriding the Update in EnemyFSMState_Movement so this is called with this enemies specific traits.
        /// </summary>
        public override void Update()
        {
            base.Update();

            thisEnemy.timer += Time.deltaTime;

            if (thisEnemy.timer >= thisEnemy.MoveLength)    // and if they have been moving for long enough
            {
                thisEnemy.timer = 0.0f;           // reset the timer
                thisEnemy.enemyFSM.SetCurrentState(EnemyFSMStateType.IDLE);        // set them to stop moving
                thisEnemy.IsMoving = false;
            }
        }
    }
    /// <summary> <c>MakeFSMDictionary</c>
    /// Made this function to override the one in the parent class and make this enemies own FSM dictionary.
    /// </summary>
    protected override void MakeFSMDictionary()
    {
        enemyFSM.Add(new Masked_EnemyFSMState_Idle(this));
        enemyFSM.Add(new Masked_EnemyFSMState_Movement(this));
        enemyFSM.Add(new EnemyFSMState_TakeDamage(this));
        enemyFSM.Add(new EnemyFSMState_Dead(this));

        enemyFSM.SetCurrentState(EnemyFSMStateType.IDLE);
    }
    /// <summary> <c>Update</c>
    /// Using this to apply this enemies movement instead of the default
    /// </summary>
    protected override void Update()
    {
        //targetPlayer();
        //RotateTowardDirection();
        base.Update();
    }
    /// <summary> <c>RotateTowardDirection</c>
    /// Tells the enemy where to turn towards the player.
    /// </summary>
    protected override void RotateTowardDirection()
    {
        // Need to override this so that the Maked_Enemy does not rotate

        //base.RotateTowardDirection();
        //turn off walking
        if (movement != Vector2.zero)
        {
            transform.rotation =
                Quaternion.AngleAxis(180, Vector3.up);

        }
    }
}
