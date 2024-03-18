using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using RenderPipeline = UnityEngine.Rendering.RenderPipelineManager;

public class portalScript : MonoBehaviour
{
    [SerializeField] private portalScript otherPortal;

    private GameObject cam;
    private GameObject otherCam;

    private RenderTexture tempTexture;

    private bool playerInZone;


    public GameObject GetCam()
    {
        return cam;
    }

    private void Awake()
    {
        cam = transform.GetChild(0).gameObject;
        cam.GetComponent<Camera>().fieldOfView = Camera.main.fieldOfView;
    }
    void Start()
    {
        otherCam = otherPortal.GetCam();
        tempTexture = new RenderTexture(Screen.width, Screen.height, 24, RenderTextureFormat.ARGB32);
    }

    private void LateUpdate()
    {
        if (playerInZone)
        {
            Vector3 cameraOffset = Camera.main.transform.position - cam.transform.position;
            otherCam.GetComponent<Camera>().targetTexture = tempTexture;
            otherCam.transform.localPosition = cameraOffset;
            otherCam.transform.localRotation = PlayerMovement.instance.gameObject.transform.localRotation * Quaternion.Euler(Vector3.up) * MouseLook.instance.gameObject.transform.localRotation;

            cam.transform.localPosition = Vector3.zero;
            transform.GetChild(1).gameObject.GetComponent<MeshRenderer>().material.mainTexture = tempTexture;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        Vector3 offset = PlayerMovement.instance.transform.position - transform.position;
        PlayerMovement.instance.transform.position = otherPortal.transform.position + offset;
        PlayerMovement.instance.gameObject.GetComponent<Rigidbody>().velocity = Vector3.Scale(PlayerMovement.instance.gameObject.GetComponent<Rigidbody>().velocity, otherPortal.transform.forward);
        PlayerMovement.instance.transform.Rotate(Vector3.up);
        MouseLook.instance.transform.rotation = otherCam.transform.rotation;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == PlayerMovement.instance.gameObject)
        {
            playerInZone = true;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == PlayerMovement.instance.gameObject)
        {
            playerInZone = false;
        }
    }
}
