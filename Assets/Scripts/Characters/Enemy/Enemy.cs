using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Patterns;     // used to find the FSM scripts

/// <summary>
/// <c>Enemy</c> holds default methods, FSM states, and fields of Enemies. Meant to be inherited from by other Enemy classes
/// </summary>
public class Enemy : Entity
{

    // variables for all the state classes to use
    public float timer { get; set; } = 0.0f;


    // Unity cannot serialize fields with getters and setters, so these have separate fields that act as getters/setters.
    [SerializeField]
    protected bool isMoving = false;       // set the object to be moving or not moving initially
    [SerializeField]
    private float moveDelay = 0.0f;       // how many seconds the enemy will wait between moving toward the player.
    [SerializeField]
    private float moveLength = 1.0f;       // how many seconds the enemy will move towards the player before delaying again

    public float MoveDelay { get => moveDelay; private set => moveDelay = value; }
    public float MoveLength { get => moveLength; private set => moveLength = value; }
    public bool IsMoving { get => isMoving; set => isMoving = value; }

    /// <summary>
    /// This enum is crucial to the Finite State Machine. These are 'keys' which the FSM uses to distinguish what state the object is currently in.
    /// </summary>
    public enum EnemyFSMStateType
    {
        MOVEMENT = 0,
        IDLE,
        ATTACK,
        TAKE_DAMAGE,  // not used yet, as no animation for this state
        DEAD,
    }

    /// <summary>
    /// This is a base class for all FSM states to derive from. Its functions will be overridden to make the different states function uniquely.
    /// However, many of the States established in this script exist to be further overridden by classes which inherit the Enemy class. i.e. Slime.
    /// </summary>
    public class EnemyFSMState : State<int>
    {
 
        // we will keep the ID for state for convenience
        // this id represents the key
        public new EnemyFSMStateType ID { get { return _id; } }

        protected Enemy thisEnemy = null;
        protected EnemyFSMStateType _id;

        /// <summary>
        /// This function gives the FSM an Enemy object to access the Enemy's fields through. Called thisEnemy
        /// </summary>
        /// <param name="fsm"> The FSM object created to manage the states. It will have its base contructor called </param>
        /// <param name="enemy"> The Enemy object which you want to manage the states of </param>
        public EnemyFSMState(FiniteStateMachine<int> fsm, Enemy enemy) : base(fsm)
        {
            thisEnemy = enemy;
        }

        /// <summary>
        /// This function gives the FSM an Enemy object to access the Enemy's fields through. Named thisEnemy.
        /// </summary>
        /// <param name="enemy"> The Enemy object which you want to manage the states of </param>
        public EnemyFSMState(Enemy enemy) : base()
        {
            thisEnemy = enemy;
            m_fsm = thisEnemy.enemyFSM;
        }

        /// <summary>
        /// This gets called upon entering the state. For now, it simply calls the base function.
        /// </summary>
        public override void Enter()
        {
            base.Enter();
        }

        /// <summary>
        /// This gets called upon exiting the state. For now, it simply calls the base function.
        /// </summary>
        public override void Exit()
        {
            base.Exit();
        }

        /// <summary>
        /// While in the state, this gets called every frame. For now, it simply calls the base function.
        /// </summary>
        public override void Update()
        {
            base.Update();
        }

        /// <summary>
        /// While in the state, this gets called repeatedly after a fixed amount of . For now, it simply calls the base function.
        /// </summary>
        public override void FixedUpdate()
        {
            base.FixedUpdate();
        }

    }

    /// <summary>
    /// A state for when the Enemy is Idle and does not have a specific 'goal' in terms of gameplay.
    /// i.e it is not moving or has not encountered the player yet.
    /// </summary>
    public class EnemyFSMState_Idle : EnemyFSMState
    {
        /// <summary>
        /// Constructor that sets this State's enum ID to IDLE. This is so the FSM can see what key it possesses.
        /// </summary>
        /// <param name="enemy"> The enemy object which must be passed to the base constructor so that it may reference the enemy's fields</param>
        public EnemyFSMState_Idle(Enemy enemy) : base(enemy)
        {
            _id = EnemyFSMStateType.IDLE;
        }

        /// <summary>
        /// This function is called every frame and checks the isMoving bool. If its true, the State is set to MOVEMENT
        /// </summary>
        public override void Update()
        {
            base.Update();
            if (thisEnemy.IsMoving)
                thisEnemy.enemyFSM.SetCurrentState(EnemyFSMStateType.MOVEMENT);
        }
    }

    /// <summary>
    /// A state for when the Enemy is moving. By default, it will simply move the enemy through fixed update for as long as it is in this State
    /// </summary>
    public class EnemyFSMState_Movement : EnemyFSMState
    {
        /// <summary>
        /// Constructor that sets this State's enum ID to MOVEMENT. This is so the FSM can see what key it possesses.
        /// </summary>
        /// <param name="enemy"> The enemy object which must be passed to the base constructor so that it may reference the enemy's fields</param>
        public EnemyFSMState_Movement(Enemy enemy) : base(enemy)
        {
            _id = EnemyFSMStateType.MOVEMENT;
        }

        /// <summary>
        /// Calls the base FixedUpdate and then moves the Enemy towards the currently stored direction
        /// </summary>
        public override void FixedUpdate()
        {
            base.FixedUpdate();
            thisEnemy.Movement(thisEnemy.direction);    // move them towards their target
        }
    }

    /// <summary>
    /// A state for when the Enemy is attacking.
    /// </summary>
    public class EnemyFSMState_Attack : EnemyFSMState
    {
        /// <summary>
        /// Constructor that sets this State's enum ID to ATTACK. This is so the FSM can see what key it possesses.
        /// </summary>
        /// <param name="enemy"> The enemy object which must be passed to the base constructor so that it may reference the enemy's fields</param>
        public EnemyFSMState_Attack(Enemy enemy) : base(enemy)
        {
            _id = EnemyFSMStateType.ATTACK;
        }

    }

    /// <summary>
    /// A state for when the enemy is taking damage. 
    /// This can handle triggers such as "upon taking damage, drop blood on the ground."
    /// </summary>
    public class EnemyFSMState_TakeDamage : EnemyFSMState
    {
        /// <summary>
        /// Constructor that sets this State's enum ID to TAKE_DAMAGE. This is so the FSM can see what key it possesses.
        /// </summary>
        /// <param name="enemy"> The enemy object which must be passed to the base constructor so that it may reference the enemy's fields</param>
        public EnemyFSMState_TakeDamage(Enemy enemy) : base(enemy)
        {
            _id = EnemyFSMStateType.TAKE_DAMAGE;
        }

        public override void Enter()
        {
            base.Enter();

            thisEnemy.HealthReduce(10);
            if (thisEnemy.CurrentHealth <= 0)
                thisEnemy.enemyFSM.SetCurrentState(EnemyFSMStateType.DEAD);
            thisEnemy.enemyFSM.SetCurrentState((EnemyFSMStateType) 0);      // return it to the default state
        }

    }

    /// <summary>
    /// A state for when the enemy has died. 
    /// This will most likely simply delete the GameObject, but it can handle triggers such as "upon dying, explode."
    /// </summary>
    public class EnemyFSMState_Dead : EnemyFSMState
    {
        /// <summary>
        /// Constructor that sets this State's enum ID to DEAD. This is so the FSM can see what key it possesses.
        /// </summary>
        /// <param name="enemy"> The enemy object which must be passed to the base constructor so that it may reference the enemy's fields</param>
        public EnemyFSMState_Dead(Enemy enemy) : base(enemy)
        {
            _id = EnemyFSMStateType.DEAD;
        }

        /// <summary>
        /// This gets called upon entering the state. It destroys the enemy.
        /// </summary>
        public override void Enter()
        {
            base.Enter();
            Destroy(thisEnemy.gameObject);
        }
    }


    /// <summary>
    /// This class gives the Enemy class better access to the generic FSM class.
    /// Through the objects of this class, Enemy objects can add State keys to an FSM dictionary and set their current State key.
    /// </summary>
    public class EnemyFSM : FiniteStateMachine<int>
    {   
        /// <summary>
        /// Calls the base constructor, which makes a dictionary of enumerated State keys
        /// </summary>
        public EnemyFSM() : base()
        { }

        /// <summary>
        /// Adds the State's ID and correlating EnemyFSMState object to the dictionary.
        /// The dictionary will then be used by checking the ID key, and sending the compiler to the related object so that it may run its methods.
        /// </summary>
        /// <param name="state"> The State object which holds the State ID and the methods related to that state. 
        /// i.e an object belonging to EnemyFSMState_Movement </param>
        public void Add(EnemyFSMState state)
        {
            m_states.Add((int)state.ID, state);
        }

        /// <summary>
        /// When sent an enumerated data type from the FSM dictionary, this function will return an object which correlates to the 'key' which was passed
        /// i.e. passing MOVEMENT will return an object of EnemyFSMState_Movement
        /// </summary>
        /// <param name="key"> The enumerated State key which describes the State the object is in. i.e IDLE, MOVEMENT, DEAD, etc </param>
        /// <returns></returns>
        public EnemyFSMState GetState(EnemyFSMStateType key)
        {
            // calls an overloaded function from the base class
            return (EnemyFSMState)GetState((int)key);
        }

        /// <summary>
        /// Changes the state key of the object, changing which Update, FixedUpdate, and other functions it will use to the ones which belong to the new state.
        /// </summary>
        /// <param name="stateKey"> The enumerated State key which describes the State the object is in. i.e IDLE, MOVEMENT, DEAD, etc </param>
        public void SetCurrentState(EnemyFSMStateType stateKey)
        {
            State<int> state = m_states[(int)stateKey];
            if (state != null)
            {
                SetCurrentState(state);
            }
        }
    }


    public Transform target; // we need the player's position

    public EnemyFSM enemyFSM = null;

    /// <summary>
    /// Essentially Unity's Constructor. Gets called upon creation of the GameObject.
    /// Calls Entity's constructor, initializing collider and animator components, 
    /// creates the EnemyFSM object and FSM dictionary, and 
    /// sets the objects 'target' to be a Game Object with the "Player" tag.
    /// </summary>
    protected override void Awake()
    {
        // create the rigidbody and animator components
        base.Awake();

        // create the FSM
        enemyFSM = new EnemyFSM();

        // set the target of the enemy to a game object with the "player" tag
        target = GameObject.FindWithTag("Player").transform;

        // create the FSM dictionary
        MakeFSMDictionary();
    }

    /// <summary>
    /// A function that adds a set of States to the dictionary. 
    /// It is meant to be overridden so that unique enemies may add their own unique States to their respective dictionary.
    /// Sets the default State to MOVEMENT
    /// </summary>
    protected virtual void MakeFSMDictionary()
    {
        // add all the enemy states to the FSM dictionary
        enemyFSM.Add(new EnemyFSMState_Idle(this));
        enemyFSM.Add(new EnemyFSMState_Movement(this));
        enemyFSM.Add(new EnemyFSMState_Attack(this));
        enemyFSM.Add(new EnemyFSMState_TakeDamage(this));
        enemyFSM.Add(new EnemyFSMState_Dead(this));

        // set the state to moving by default
        enemyFSM.SetCurrentState(EnemyFSMStateType.MOVEMENT);
    }

    /// <summary>
    /// Update gets called on the object every frame.
    /// This function makes sure to update the objects current direction with the new location of the target.
    /// It then calls the FSM update to take the compiler to the Update() function of its current State.
    /// </summary>
    protected virtual void Update()
    {
        targetPlayer();

        enemyFSM.Update();
    }

    /// <summary>
    /// Updates the current direction of the object to be pointing to its target, 
    /// rotates the enemy to face that target, and
    /// normalizes the vector pointing to that target.
    /// </summary>
    protected void targetPlayer()
    {
        direction = target.position - transform.position; // find direction vector from enemy to player
        RotateTowardDirection(); // rotate enemy sprite to face player
        moveCharacter(ref direction); // normalize the direction vector and set this to the movement vector
    }

    /// <summary>
    /// Use FixedUpdate() for physics. Instead of every frame, it is caused at a fixed interval, making physics consitent rather than tied to framerate.
    /// This function simply calls the FSM's FixedUpdate which should redirect to the current State's FixedUpdate
    /// </summary>
    protected override void FixedUpdate()
    {
        enemyFSM.FixedUpdate();
    }

    /// <summary>
    /// This normalizes the vector and makes it so that the enemy can have their sprite properly point towards the player
    /// </summary>
    /// <param name="direction"> The direction vector the enemy will be moving along </param>
    public void moveCharacter(ref Vector2 direction)
    {

        direction.Normalize(); // really cool vector normalization function in Unity
        movement = direction; // Vector2D movement is now the normalized vector
    }

    /// <summary>
    /// This function runs when Unity detects that a game object has collided with this one. There is no reason to call this function independantly.
    /// When an enemy collides with something, it will check to see if it was that Player's sword. If it was, it will shift to the TakeDamage State
    /// </summary>
    /// <param name="col"> The specific instance of collision that was detected. Gets passed automatically by Unity. </param>
    protected override void OnCollisionEnter2D(Collision2D col)  // if they hit something
    {
        if (col.gameObject.tag == "SwordHitbox")
        {
            // EnemyFSMStateType previousState = (EnemyFSMStateType)enemyFSM.GetCurrentState().ID;
            enemyFSM.SetCurrentState(EnemyFSMStateType.TAKE_DAMAGE);
            // enemyFSM.SetCurrentState(previousState); 
        }
    }
}