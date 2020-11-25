using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public InputMaster controls;
    Vector2 mousePos;
    [SerializeField]
    float mouseSensitivity = 100f;
    public Transform playerBody;
    float xRotation = 0f;

    void Awake()
    {
        controls = new InputMaster();
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = controls.Player.MousePosition.ReadValue<Vector2>() * mouseSensitivity * Time.deltaTime;

        xRotation -= mousePos.y;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        //transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mousePos.x);
    }

    private void OnEnable()
    {
        controls.Enable();
    }
    private void OnDisable()
    {
        controls.Disable();
    }
}
