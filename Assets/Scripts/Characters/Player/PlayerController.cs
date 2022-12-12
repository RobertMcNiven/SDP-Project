using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerControls playerControls;

    private void Awake()
    {
        playerControls = new PlayerControls();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    void Start()
    {

    }

    private void Update()
    {
        Vector2 move = playerControls.Player.Move.ReadValue<Vector2>();
        //Debug.Log(move);
        playerControls.Player.Attack.ReadValue<float>();
        if (playerControls.Player.Attack.triggered == true)
            Debug.Log("Attack");

    }

}
