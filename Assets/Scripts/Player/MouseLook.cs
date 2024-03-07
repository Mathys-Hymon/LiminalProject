using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
using UnityEngine.Windows;

public class MouseLook : MonoBehaviour
{
    public static MouseLook instance;


    [SerializeField] private float sensitivity = 10f;
    [SerializeField] private float xClamp = 85f;
    private float xRotation;
    private Vector2 input;

    private void Start()
    {
        instance = this;
    }

    public void Interact()
    {

    }

    public void ReceiveInput(Vector2 mouseInput)
    {
        input = mouseInput * sensitivity;
    }

    private void Update()
    {

        xRotation -= ((input.y / 10) * sensitivity);
        xRotation = Mathf.Clamp(xRotation, -xClamp, xClamp);
        var VerticalRot = Quaternion.AngleAxis(xRotation, Vector3.right);
        transform.localRotation = VerticalRot;

        PlayerMovement.instance.transform.Rotate((Vector3.up * (input.x / 10) * sensitivity));
    }
}
