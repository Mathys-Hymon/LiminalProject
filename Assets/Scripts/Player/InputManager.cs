using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private PlayerInput controls;
    private PlayerInput.GroundMovementActions groundMovement;

    private Vector2 horizontalInput;
    private Vector2 mouseInput;

    private void Awake()
    {
        controls = new PlayerInput();
        groundMovement = controls.GroundMovement;

        groundMovement.HorizontalMovement.performed += ctx => horizontalInput = ctx.ReadValue<Vector2>();
        groundMovement.Jump.performed += _ => PlayerMovement.instance.OnJumpPressed();
        groundMovement.Interact.started += _ => MouseLook.instance.Interact(true);
        groundMovement.Interact.canceled += _ => MouseLook.instance.Interact(false);
        groundMovement.MouseX.performed += ctx => mouseInput.x = ctx.ReadValue<float>();
        groundMovement.MouseY.performed += ctx => mouseInput.y = ctx.ReadValue<float>();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        PlayerMovement.instance.ReceiveInput(horizontalInput);
        MouseLook.instance.ReceiveInput(mouseInput);
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDestroy()
    {
        controls.Disable();
    }
}
