///<summary>
/// Player.cs
/// 
/// This file contains the code for the Player object.
/// It includes the Player class, along with the Player's States that are 
/// necessary for the state machine.
/// 
/// Bugs and Features:
/// No known bugs
/// Some states aren't utilized in the game yet.
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Patterns;  // finite state machine

/// <summary>
/// <c>PlayerFSMStateType</c> Enumeration list for easy references to all of the available states.
/// </summary>
// number all the states
public enum PlayerFSMStateType
{
    MOVEMENT = 0,
    ATTACK,
    DEFEND,
    SKILL,
    TAKE_DAMAGE,
    DEAD,
}

/// <summary>
/// <c>PlayerFSMState</c> A template state with functions needed for the Player's FSM states.
/// Adds Player object specific functionality to a generic State{T} class.
/// 
/// <remarks>Template State for Player class</remarks>
/// /// <see cref="State{T}">
/// </summary>
public class PlayerFSMState : State<int>
{
    // we will keep the ID for state for convenience
    // this id represents the key
    public new PlayerFSMStateType ID { get { return _id; } }
    protected Player _player = null;
    protected PlayerFSMStateType _id;

    /// <summary>
    /// Constructor taking an FSM object and a Player object, while calling the base State{T} constructor.
    /// 
    /// </summary>
    /// <param name="fsm">The Player's FiniteStateMachine object</param>
    /// <param name="player">The player object</param>
    public PlayerFSMState(FiniteStateMachine<int> fsm, Player player) : base(fsm)
    {
        _player = player;
    }


    /// <summary>
    /// Convenience constructor with just Player.
    /// Assigns the player object we passed to our null _player initializer, along with the Player's FSM
    /// </summary>
    /// <param name="player">A player object</param>
    public PlayerFSMState(Player player) : base()
    {
        _player = player;
        m_fsm = _player.playerFSM;
    }

    /// <inheritdoc/>
    /// <summary>
    /// <c>Enter</c>What's called when we enter the state.
    /// </summary>
    public override void Enter()
    {
        base.Enter();
    }

    /// <inheritdoc/>
    /// <summary>
    /// <c>Exit</c>What's called when we leave the state.
    /// </summary>
    public override void Exit()
    {
        base.Exit();
    }

    /// <inheritdoc/>
    /// <summary>
    /// <c>Update</c>Update is called nearly 200 times a second.
    /// </summary>
    public override void Update()
    {
        base.Update();
    }

    /// <inheritdoc/>
    /// <summary>
    /// <c>FixedUpdate</c>FixedUpdate is called exactly 50 times a second on any machine.
    /// </summary>
    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

}

/// <summary>
/// The Movement State for the player.
/// </summary>
public class PlayerFSMState_MOVEMENT : PlayerFSMState
{
    /// <summary>
    /// Call the base constructor and assign the ID for Movement
    /// </summary>
    /// <param name="player">the Player Object</param>
    public PlayerFSMState_MOVEMENT(Player player) : base(player)
    {
        _id = PlayerFSMStateType.MOVEMENT;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();
        //Debug.Log("In movement function");
        // when we press the attack button, this should switch to the attack state
        // Unity input handling sends messages to functions similar to these
        // this might need to be moved
        if (_player.attackPressed == true)
        {
            _player.playerFSM.SetCurrentState(PlayerFSMStateType.ATTACK);
        }
        else if (_player.defendPressed)
        {
            _player.playerFSM.SetCurrentState(PlayerFSMStateType.DEFEND);

        }
        else if (_player.skillOnePressed)
        {
            _player.playerFSM.SetCurrentState(PlayerFSMStateType.SKILL);
        }
        else if (_player.skillTwoPressed)
        {
            _player.playerFSM.SetCurrentState(PlayerFSMStateType.SKILL);
        }
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        _player.Move();
    }
}

/// <summary>
/// Attack State for Player
/// </summary>
public class PlayerFSMState_ATTACK : PlayerFSMState
{
    /// <summary>
    /// sets ID for the state and calls base contructor
    /// </summary>
    /// <param name="player">The Player Object</param>
    public PlayerFSMState_ATTACK(Player player) : base(player)
    {
        _id = PlayerFSMStateType.ATTACK;
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("PlayerFSMState_ATTACK");
        _player.anim.SetBool("attack", true);

        Vector2 playerPos = _player.transform.position;
        Vector2 playerDirection = _player.transform.forward;
        Quaternion playerRotation = _player.transform.rotation;
        float spawnDistance = 100;

        Vector2 spawnPos = playerPos + (playerDirection * spawnDistance);
        _player.sword.transform.position = spawnPos;
        _player.sword.col.enabled = true;
    }
    public override void Exit()
    {
        base.Exit();
        _player.anim.SetBool("attack", false);

        _player.sword.col.enabled = false;
    }
    public override void Update()
    {
        base.Update();
        //Debug.Log("In attack function");
        if (_player.attackPressed == false)
        {
            _player.anim.SetBool("attack", false);
            _player.playerFSM.SetCurrentState(PlayerFSMStateType.MOVEMENT);
        }
    }
    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }
}

/// <summary>
/// Skill State for player (no function yet)
/// </summary>
public class PlayerFSMState_SKILL : PlayerFSMState
{
    /// <summary>
    /// grabs ID for state and calls base contructor
    /// </summary>
    /// <param name="player">The player object</param>
    public PlayerFSMState_SKILL(Player player) : base(player)
    {
        _id = PlayerFSMStateType.SKILL;
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("PlayerFSMState_SKILL");
        if (_player.skillOnePressed)
        {
            _player.anim.SetBool("skill_1", true);
        }
        else if (_player.skillTwoPressed)
        {
            _player.anim.SetBool("skill_2", true);
        }

    }
    public override void Exit()
    {
        _player.anim.SetBool("skill_1", false);
        _player.anim.SetBool("skill_2", false);
    }

    public override void Update()
    {
        //Debug.Log("In skill function");
        if ((_player.skillOnePressed == false) && (_player.skillTwoPressed == false))
        {
            _player.anim.SetBool("skill_1", false);
            _player.anim.SetBool("skill_2", false);
            _player.playerFSM.SetCurrentState(PlayerFSMStateType.MOVEMENT);
        }
    }
    public override void FixedUpdate() { }
}

/// <summary>
/// The Defend state for player (No function)
/// </summary>
public class PlayerFSMState_DEFEND : PlayerFSMState
{
    /// <summary>
    /// Grabs ID for state
    /// </summary>
    /// <param name="player">the Player object</param>
    public PlayerFSMState_DEFEND(Player player) : base(player)
    {
        _id = PlayerFSMStateType.DEFEND;
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("PlayerFSMState_DEFEND");
        _player.anim.SetBool("defend", true);
    }
    public override void Exit()
    {
        _player.anim.SetBool("defend", false);
    }
    public override void Update()
    {

        //Debug.Log("In defend function");
        if (_player.defendPressed == false)
        {
            _player.anim.SetBool("defend", false);
            _player.playerFSM.SetCurrentState(PlayerFSMStateType.MOVEMENT);
        }

    }
    public override void FixedUpdate() { }
}

/// <summary>
/// the Take_Damage state, where player takes damage
/// </summary>
public class PlayerFSMState_TAKE_DAMAGE : PlayerFSMState
{
    /// <summary>
    /// Grabs ID for state
    /// </summary>
    /// <param name="player">The Player Object</param>
    public PlayerFSMState_TAKE_DAMAGE(Player player) : base(player)
    {
        _id = PlayerFSMStateType.TAKE_DAMAGE;
    }

    public override void Enter() {
        //reduces player health
        _player.HealthReduce(10);
    }
    public override void Exit() { }
    public override void Update()
    {
        Debug.Log("In take_damage function");
    }
    public override void FixedUpdate() {
        _player.playerFSM.SetCurrentState(PlayerFSMStateType.MOVEMENT);
    }
}

/// <summary>
/// The Dead State for player when health reaches 0
/// </summary>
public class PlayerFSMState_DEAD : PlayerFSMState
{
    /// <summary>
    /// ID constructor
    /// </summary>
    /// <param name="player">Player Object</param>
    public PlayerFSMState_DEAD(Player player) : base(player)
    {
        _id = PlayerFSMStateType.DEAD;
    }

    public override void Enter()
    {
        Debug.Log("Player dead");
        _player.anim.SetTrigger("die");

        // Need to use this to change scenes within uniity
        UnityEngine.SceneManagement.SceneManager.LoadScene("Lose");
        //Destroy(_player);
    }
    public override void Exit() { }
    public override void Update()
    {
        Debug.Log("In dead function");
    }
    public override void FixedUpdate() {
        Debug.Log("Player also dead");
    }
}

/// <summary>
/// Gives the Player class better access to the generic FSM class
/// 
/// </summary>
public class PlayerFSM : FiniteStateMachine<int>
{
    /// <summary>
    /// <c>PlayerFSM</c>
    /// calls the base constructor, which makes a dictionary of ints
    /// </summary>
    public PlayerFSM() : base() { }

    /// <summary>
    /// <c>Add</c>adds the state to the dictionary of states
    /// </summary>
    /// <param name="state">the state we want to add</param>
    public void Add(PlayerFSMState state)
    {
        m_states.Add((int)state.ID, state);
    }

    /// <summary>
    /// <c>GetState</c>Returns the state when given a key in the PlayerFSMStateType enum.
    /// Basically a helper function for the Player object
    /// </summary>
    /// <param name="key">the state listed in the <see cref="PlayerFSMStateType"/></param>
    /// <returns>The state casted to PlayerFSMState</returns>
    public PlayerFSMState GetState(PlayerFSMStateType key)
    {
        return (PlayerFSMState)GetState((int)key);
    }

    /// <summary>
    /// <c>SetCurrentState</c>sets the current state in the PlayerFSM to the desired state in the <see cref="PlayerFSMStateType"/>
    /// </summary>
    /// <param name="stateKey">the key in the <see cref="PlayerFSMStateType"/></param>
    public void SetCurrentState(PlayerFSMStateType stateKey)
    {
        State<int> state = m_states[(int)stateKey];
        if (state != null)
        {
            SetCurrentState(state);
        }
    }
}

/// <summary>
/// This class allows for the Player object to interact with the Unity Engine.
/// 
/// </summary>
public class Player : Entity
{
    public PlayerFSM playerFSM = null;

    // Input bools for the player
    public bool attackPressed;
    public bool defendPressed;
    public bool skillOnePressed;
    public bool skillTwoPressed;

    public SwordHitbox sword;

    /// <summary>
    /// <c>Awake</c>Called in Unity when the object is enabled.
    /// This is basically the Unity equivalent of a constructor for objects
    /// that inherit from MonoBehaviour.
    /// </summary>
    protected override void Awake()
    {
        //get the Rigidbody2D and Animator in Unity
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        //set the currentHealth to the Total Health
        CurrentHealth = HealthTotal;

        //create the FSM
        playerFSM = new PlayerFSM();

        //add all the states we have to the fsm dictionary we created
        playerFSM.Add(new PlayerFSMState_MOVEMENT(this));
        playerFSM.Add(new PlayerFSMState_ATTACK(this));
        playerFSM.Add(new PlayerFSMState_SKILL(this));
        playerFSM.Add(new PlayerFSMState_DEFEND(this));
        playerFSM.Add(new PlayerFSMState_TAKE_DAMAGE(this));
        playerFSM.Add(new PlayerFSMState_DEAD(this));

        //set the state to movement by default
        playerFSM.SetCurrentState(PlayerFSMStateType.MOVEMENT);
        Debug.Log(playerFSM.GetCurrentState().ID);
    }

    /// <summary>
    /// <c>Update</c>call the current state's Update()
    /// </summary>
    protected void Update()
    {
        playerFSM.Update();
    }

    /// <summary>
    /// <c>FixedUpdate</c>call the current state's FixedUpdate()
    /// </summary>
    protected override void FixedUpdate()
    {
        playerFSM.FixedUpdate();
    }

    /// <summary>
    /// <c>OnCollisionEnter2D</c>When Player's Collision2D overlaps with another Collision2D, trigger this
    /// </summary>
    /// <param name="collision">the Collision2D that we are colliding with</param>
    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        // check the Unity tag for the Collision2D object we are touching
        if (collision.gameObject.tag == "Enemy")
        {
            Debug.Log("Player Hit");
            if (this.CurrentHealth > 0)
            {
                playerFSM.SetCurrentState(PlayerFSMStateType.TAKE_DAMAGE);
            }
            if (this.CurrentHealth == 0)
            {
                playerFSM.SetCurrentState(PlayerFSMStateType.DEAD);
            }
        }
    }

    /// <summary>
    /// <c>Move</c>Convenience Method to allow for correct player movement
    /// </summary>
    public void Move()
    {
        RotateTowardDirection();
        Movement();
    }

    /// <summary>
    /// <c>OnMove</c>Unity sends messages to this function every frame to check WASD direction
    /// </summary>
    /// <param name="value">Unity's InputValue</param>
    void OnMove(InputValue value)
    {
        // get the player's input as a float vector
        movement = value.Get<Vector2>();
    }

    /// <summary>
    /// <c>OnMove</c>Unity sends messages to this function every frame to check attack button
    /// </summary>
    /// <param name="value">Unity's InputValue</param>
    void OnAttack(InputValue value)
    {
        attackPressed = value.isPressed;
    }

    /// <summary>
    /// <c>OnMove</c>Unity sends messages to this function every frame to check defend button
    /// </summary>
    /// <param name="value">Unity's InputValue</param>
    void OnDefend(InputValue value)
    {
        defendPressed = value.isPressed;
    }

    /// <summary>
    /// <c>OnMove</c>Unity sends messages to this function every frame to check skill button
    /// </summary>
    /// <param name="value">Unity's InputValue</param>
    void OnSkill_1(InputValue value)
    {
        skillOnePressed = value.isPressed;
    }

    /// <summary>
    /// <c>OnMove</c>Unity sends messages to this function every frame to check skill 2 button
    /// </summary>
    /// <param name="value">Unity's InputValue</param>
    void OnSkill_2(InputValue value)
    {
        skillTwoPressed = value.isPressed;
    }

    /// <summary>
    /// <c>RotateTowardDirection</c>Rotate the sprite towards direction being moved, and update the Player's Animator
    /// </summary>
    protected override void RotateTowardDirection()
    {
        //turn off walking
        if (movement != Vector2.zero) // if we have player movement input
        {
            // rotate sprite to face direction of movement
            transform.rotation =
                Quaternion.LookRotation(Vector3.forward, movement);
            // turn on walking animation
            anim.SetBool("walking", true);
        }
        else
        {
            //turn off walking
            anim.SetBool("walking", false);
        }
    }
}
