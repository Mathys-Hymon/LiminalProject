using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private float speed = 11f;
    [SerializeField] private float gravity = -30f;
    [SerializeField] private float jumpHeight = 3.5f;

    [SerializeField] private LayerMask playerLayer;


    private CharacterController controller;
    private Vector2 horizontalInput;
    private Vector3 verticalVelocity = Vector3.zero;
    private bool isGrounded, isJumping;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    private void Update()
    {
        isGrounded = Physics.CheckSphere(transform.TransformPoint(controller.center - Vector3.up * (controller.height / 2)), 0.1f, ~playerLayer);
        if(isGrounded )
        {
            verticalVelocity.y = 0;
        }
        if(isJumping )
        {
            if(isGrounded)
            {
                verticalVelocity.y = Mathf.Sqrt(-2f * jumpHeight * gravity);
            }
            isJumping = false;
        }
       
        Vector3 horizontalVelocity = (transform.right * horizontalInput.x + transform.forward * horizontalInput.y) * speed;
        controller.Move(horizontalVelocity * Time.deltaTime);

        verticalVelocity.y += gravity * Time.deltaTime;
        controller.Move(verticalVelocity * Time.deltaTime);
    }
    public void ReceiveInput (Vector2 _horizontalInput)
    {
        horizontalInput = _horizontalInput;
    }
    public void OnJumpPressed()
    {
        isJumping = true; 
    }
}
