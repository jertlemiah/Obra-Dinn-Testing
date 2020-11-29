using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private InputMaster playerControls;
    private static InputManager _instance;


    public static InputManager Instance {
        get {
            return _instance;
        }
    }

    void Awake()
    {
        // There should only ever be one InputManager. May implement singleton later.
        if (_instance != null && _instance != this) {
            Destroy(this.gameObject);
        } else
        {
            _instance = this;
        }
        playerControls = new InputMaster();
        Cursor.visible = false;
    }

    public InputMaster GetInputActions()
    {
        return playerControls;
    }

    public Vector2 GetPlayerMovement()
    {
        return playerControls.Player.Movement.ReadValue<Vector2>();
    }

    public Vector2 GetMouseDelta()
    {
        return playerControls.Player.Look.ReadValue<Vector2>();
    }
    public bool PlayerJumpedThisFrame()
    {
        return playerControls.Player.Jump.triggered;
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }
    private void OnDisable()
    {
        playerControls.Disable();
    }
}
