using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement instance;

    [SerializeField] private float speed = 11f;
    [SerializeField] private float floorFriction = 1f;
    [SerializeField] private float airControl = 1f;
    [SerializeField] private float jumpHeight = 3.5f;

    [SerializeField] private LayerMask floorLayer;


    private Rigidbody rb;
    private Vector2 horizontalInput;
    private bool isGrounded, isJumping;

    private void Start()
    {
        instance = this;
        PlayerPrefs.SetInt("language", 0);
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        isGrounded = Physics.CheckSphere(transform.position - Vector3.up, 0.1f, floorLayer);

        if(isJumping)
        {
            if(isGrounded)
            {
               rb.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
            }
            isJumping = false;
        }
    }

    private void FixedUpdate()
    {
        float friction;

        if(isGrounded)
        {
            friction = 1;
            rb.drag = 8;
        }
        else
        {
            friction = 0.2f;
            rb.drag = 0.5f * airControl;
        }
        Vector3 targetVelocity = transform.TransformDirection(new Vector3(horizontalInput.x, 0, horizontalInput.y)) * speed;

        rb.velocity = new Vector3(Mathf.Clamp(rb.velocity.x, -10, 10), Mathf.Clamp(rb.velocity.y, -10, 10), Mathf.Clamp(rb.velocity.z, -10, 10));
        rb.AddForce(new Vector3(targetVelocity.x, 0, targetVelocity.z) * Time.fixedDeltaTime * 500 * friction, ForceMode.Force);
    }

    public void ReceiveInput (Vector2 _horizontalInput)
    {
        horizontalInput = _horizontalInput;
    }
    public void OnJumpPressed()
    {
        isJumping = true; 
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position - Vector3.up, 0.1f);
    }
}
