using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement instance;

    [SerializeField] private float speed = 11f;
    [SerializeField] private float floorFriction = 1f;
    [SerializeField] private float airControl = 1f;
    [SerializeField] private float gravity = -30f;
    [SerializeField] private float jumpHeight = 3.5f;

    [SerializeField] private LayerMask playerLayer;


    private CharacterController controller;
    private Vector2 horizontalInput;
    private Vector3 verticalVelocity = Vector3.zero, horizontalVelocity = Vector3.zero;
    private bool isGrounded, isJumping;

    private void Start()
    {
        instance = this;
        controller = GetComponent<CharacterController>();

    }
    private void Update()
    {
        isGrounded = Physics.CheckSphere(transform.TransformPoint(controller.center - Vector3.up * (controller.height / 2)), 0.1f, ~playerLayer);

        if(isGrounded )
        {
            verticalVelocity.y = 0;
        }
        else
        {
            verticalVelocity.y += gravity * Time.deltaTime;
        }
        if(isJumping)
        {
            if(isGrounded)
            {
                verticalVelocity.y = Mathf.Sqrt(-2f * jumpHeight * gravity);
            }
            isJumping = false;
        }

       if (horizontalInput.magnitude != 0) 
        {
            if(isGrounded)
            {
                horizontalVelocity = Vector3.ClampMagnitude(horizontalVelocity += (transform.right * horizontalInput.x + transform.forward * horizontalInput.y) * speed * Time.deltaTime * floorFriction*2, speed);
            }
            else
            {
                horizontalVelocity = Vector3.ClampMagnitude(horizontalVelocity += (transform.right * horizontalInput.x + transform.forward * horizontalInput.y) * speed * Time.deltaTime * airControl, speed);
            }
          
        }
       else
        {
            if(isGrounded)
            {
                if (horizontalVelocity.magnitude != 0)
                {
                    horizontalVelocity *= 1 - (Time.deltaTime * (floorFriction * 2));
                }
            }
            else
            {
                horizontalVelocity *= 1 - (Time.deltaTime * airControl);
            }
        }
        
        controller.Move((horizontalVelocity + verticalVelocity) * Time.deltaTime);
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
