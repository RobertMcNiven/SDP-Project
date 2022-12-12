using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is for the boss enemy object Giant Enemy Spider.
/// The Giant Enemy Spider currently moves side to side, spawning smaller spider.
/// </summary>
public class GiantEnemySpider : Enemy
{
    protected Vector2 right;

    public static GameObject staticEndTrophy;       // Static fields cant be serialized. Must be static to be called by FSM
    public GameObject endTrophy;                    // Put in the game object for the trophy through Unity

    /// <summary>
    /// This function reverses the direction the object is moving when it collides with something.
    /// The GameObject this is attached to is set to only collide with walls and the player. 
    /// </summary>
    /// <param name="col"> The specific instance of collision. Automatically passed by Unity </param>
    protected override void OnCollisionEnter2D(Collision2D col)  // if they hit something
    {
        base.OnCollisionEnter2D(col);       // this object can take damage, so call the base constructor
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

        // Initialize the static endTrophy
        staticEndTrophy = endTrophy;

        this.direction = right;         // set it to move right

        enemyFSM.SetCurrentState(EnemyFSMStateType.MOVEMENT);           //set it to move
    }

    public class GESFSMState_Dead : EnemyFSMState_Dead
    {
        /// <summary>
        /// Constructor that sets this State's enum ID to IDLE. This is so the FSM can see what key it possesses.
        /// </summary>
        /// <param name="enemy"> The enemy object which must be passed to the base constructor so that it may reference the enemy's fields</param>
        public GESFSMState_Dead(Enemy enemy) : base(enemy)
        {
            _id = EnemyFSMStateType.DEAD;
        }

        /// <summary>
        /// This gets called upon entering the state. It spawns the game over trophy then calls base Enter to destroy the GES.
        /// </summary>
        public override void Enter()
        {
            Instantiate(GiantEnemySpider.staticEndTrophy);     // when the spider dies, it drops the game over trophy
            base.Enter();
        }
    }

    /// <summary>
    /// A function that adds a set of States to the dictionary. 
    /// Spider has Movement, TakeDamage, and Dead.
    /// Sets the default State to MOVEMENT
    /// </summary>
    protected override void MakeFSMDictionary()
    {
        // add all the enemy states to the FSM dictionary
        enemyFSM.Add(new EnemyFSMState_Movement(this));
        enemyFSM.Add(new EnemyFSMState_TakeDamage(this));
        enemyFSM.Add(new GESFSMState_Dead(this));

        // set the state to moving by default
        enemyFSM.SetCurrentState(EnemyFSMStateType.MOVEMENT);
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
        enemyFSM.Update();              // dont allow it to change directions
    }
}

