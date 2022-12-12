/// <summary>
/// FiniteStateMachine.cs
/// 
/// This file contains a generic FiniteStateMachine class meant to be used with the State{T} class.
/// Use with State.cs.
/// Finite State Machine
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

/// <summary>
/// <c>Patterns</c>namespace containing the FiniteStateMachine{T} class and the State{T} class
/// </summary>
namespace Patterns
{
    /// <summary>
    /// <c>FiniteStateMachine</c>Generic class to support a finite state machine control flow algorithm.
    /// 
    /// Anytime you think an object would have a state, you would want to implement this.
    /// A state is the actions an object can perform at any given time.
    /// (i.e. when the player is on the ground, he can run or jump)
    /// 
    /// Use with State class.
    /// 
    /// The FSM allows for high maintainability of the code when the program becomes complex.
    /// It reduces the need for if-else checks by substituting certain functions under certain conditions.
    /// 
    /// Don't modify this function. Inherit from it and add additional helper functions in that class.
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class FiniteStateMachine<T>
    {
        // dictionary for all possible states
        protected Dictionary<T, State<T>> m_states;
        // the current state
        protected State<T> m_currentState;


        /// <summary>
        /// <c>FiniteStateMachine</c>Creates a new dictionary for the FSM
        /// </summary>
        public FiniteStateMachine()
        {
            m_states = new Dictionary<T, State<T>>();
        }

        /// <summary>
        /// <c>Add</c>Add a state to the dictionary
        /// </summary>
        /// <param name="state">the state needed to be added</param>
        public void Add(State<T> state)
        {
            m_states.Add(state.ID, state);
        }

        /// <summary>
        /// <c>Add</c>Overloaded add to dict function
        /// </summary>
        /// <param name="stateID">The ID of the state</param>
        /// <param name="state">The state</param>
        public void Add(T stateID, State<T> state)
        {
            m_states.Add(stateID, state);
        }
        
        /// <summary>
        /// <c>GetState</c> Returns a state using an id if that state exists in the dict
        /// </summary>
        /// <param name="stateID">the desired state's id</param>
        /// <returns>the state</returns>
        public State<T> GetState(T stateID)
        {
            if (m_states.ContainsKey(stateID))
            {
                return m_states[stateID];
            }
            return null;
        }

        /// <summary>
        /// <c>SetCurrentState</c>set the current state to the desired state using id
        /// </summary>
        /// <param name="stateID">the id of the desired state</param>
        public void SetCurrentState(T stateID)
        {
            State<T> state = m_states[stateID];
            SetCurrentState(state);
        }

        /// <summary>
        /// <c>GetCurrentState</c>Gets the current state
        /// </summary>
        /// <returns>the current state</returns>
        public State<T> GetCurrentState()
        {
            return m_currentState;
        }

        /// <summary>
        /// <c>SetCurrentState</c>Sets the current state using the state.
        /// Has additional checks to call the Enter() and Exit() functions.
        /// </summary>
        /// <param name="state">the state</param>
        public void SetCurrentState(State<T> state)
        {
            //if no state change, don't set anything
            if (m_currentState == state)
            {
                return;
            }

            // if we have left the previous state, trigger Exit()
            if (m_currentState != null)
            {
                m_currentState.Exit();
            }

            // set the current state to the actual current state
            m_currentState = state;

            // now trigger the Enter() function
            if (m_currentState != null)
            {
                m_currentState.Enter();
            }
        }

        /// <summary>
        /// <c><Update/c>calls the current state's Update function
        /// </summary>
        public void Update()
        {
            if (m_currentState != null)
            {
                m_currentState.Update();
            }
        }

        /// <summary>
        /// <c>FixedUpdate</c>calls the current state's FixedUpdate function
        /// </summary>
        public void FixedUpdate()
        {
            if (m_currentState != null)
            {
                m_currentState.FixedUpdate();
            }
        }
    }
}
