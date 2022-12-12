/// <summary>
/// State.cs
/// 
/// This file contains the generic template for a state.
/// To be used with FiniteStateMachine.cs.
/// 
/// /// Finite State Machine
///  - a set of states
///  - at any given time, the FSM can only be in one of these states
///  - we can control the logic of each state
/// we will use a dictionary to hold the possible states
/// 
/// Bugs:
/// No known bugs.
/// 
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Patterns
{
    /// <summary>
    /// <c>State</c> template class for a state. Each state should inherit from this.
    /// 
    /// </summary>
    /// <typeparam name="T">Any desired data type, usually int</typeparam>
    public class State<T>
    {
        protected FiniteStateMachine<T> m_fsm;

        // name of state
        public string Name { get; set; }
        // ID of state
        public T ID { get; private set; }

        /// <summary>
        /// <c>State</c>Default constructor for State
        /// </summary>
        public State()
        {
            //Debug.Log("I think something went wrong in the parameterless State() constructor.");
        }

        /// <summary>
        /// <c>State</c>constructor for state that takes a FiniteStateMachine object
        /// </summary>
        /// <param name="fsm">the FiniteStateMachine object</param>
        public State(FiniteStateMachine<T> fsm)
        {
            m_fsm = fsm;
        }

        /// <summary>
        /// <c>State</c>constructor that takes the id of the state
        /// </summary>
        /// <param name="id">the id</param>
        public State(T id)
        {
            ID = id;
        }
         /// <summary>
         /// <c>State</c>constructor that takes the id and name of a state
         /// </summary>
         /// <param name="id">the id number of the state</param>
         /// <param name="name">the name of the state</param>
        public State(T id, string name) : this(id)
        {
            Name = name;
        }

        // delegates are basically C# function pointers from C++
        // a delegate is a reference type variable that holds the reference to a method.
        public delegate void DelegateNoArg();

        public DelegateNoArg OnEnter;
        public DelegateNoArg OnExit;
        public DelegateNoArg OnUpdate;
        public DelegateNoArg OnFixedUpdate;

        /// <summary>
        /// <c>State</c>constructor that takes multiple delegate arguments for its functions
        /// </summary>
        /// <param name="id">the id</param>
        /// <param name="onEnter">onEnter function pointer</param>
        /// <param name="onExit">on Exit function pointer</param>
        /// <param name="onUpdate">onUpdate function pointer</param>
        /// <param name="onFixedUpdate">onFixedUpdate function Pointer</param>
        public State(T id,
            DelegateNoArg onEnter,
            DelegateNoArg onExit = null,
            DelegateNoArg onUpdate = null,
            DelegateNoArg onFixedUpdate = null) : this(id)
        {
            OnEnter = onEnter;
            OnExit = onExit;
            OnUpdate = onUpdate;
            OnFixedUpdate = onFixedUpdate;
        }

        /// <summary>
        /// <c>State</c>constructor that takes multiple delegate arguments for its functions
        /// </summary>
        /// <param name="id">the id</param>
        /// <param name="name">the name of the state</param>
        /// <param name="onEnter">onEnter function pointer</param>
        /// <param name="onExit">on Exit function pointer</param>
        /// <param name="onUpdate">onUpdate function pointer</param>
        /// <param name="onFixedUpdate">onFixedUpdate function Pointer</param>
        public State(T id,
            string name,
            DelegateNoArg onEnter,
            DelegateNoArg onExit = null,
            DelegateNoArg onUpdate = null,
            DelegateNoArg onFixedUpdate = null) : this(id, name)
        {
            OnEnter = onEnter;
            OnExit = onExit;
            OnUpdate = onUpdate;
            OnFixedUpdate = onFixedUpdate;
        }

        /// <summary>
        /// <c>Enter</c>The Enter function created in FiniteStateMachine
        /// </summary>
        public virtual void Enter()
        {
            OnEnter?.Invoke();
        }

        /// <summary>
        /// <c>Exit</c>The Exit function created in FiniteStateMachine
        /// </summary>
        public virtual void Exit()
        {
            OnExit?.Invoke();
        }

        /// <summary>
        /// <c>Update</c>Unity's update function
        /// </summary>
        public virtual void Update()
        {
            OnUpdate?.Invoke();
        }

        /// <summary>
        /// <c>FixedUpdate</c>Unity's FixedUpdate function
        /// </summary>
        public virtual void FixedUpdate()
        {
            OnFixedUpdate?.Invoke();
        }
    }
}
