using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField] private float sensitivity = 10f;
    [SerializeField] private float xClamp = 85f;
    private Transform playerCamera;
    private float mouseX, mouseY, xRotation;

    private void Start()
    {
        playerCamera = transform.GetChild(0).transform;
    }

    public void ReceiveInput (Vector2 mouseInput)
    {
        mouseX = (mouseInput.x * sensitivity);
        mouseY = (mouseInput.y * sensitivity);
    }

    private void Update()
    {
        transform.Rotate(Vector3.up * mouseX * Time.deltaTime);
        xRotation -= mouseY * Time.deltaTime;
        xRotation = Mathf.Clamp(xRotation, -xClamp, xClamp);
        Vector3 targetRotation = transform.eulerAngles;
        targetRotation.x = xRotation;
        playerCamera.eulerAngles = targetRotation;
    }
}
