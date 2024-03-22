using UnityEngine;


public class portalScript : MonoBehaviour
{
    [SerializeField] private portalScript otherPortal;
    [SerializeField] private float DistanceToRender;

    private GameObject cam;
    private GameObject otherCam;

    private RenderTexture tempTexture;

    private bool playerOverlapping;


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

    private void Update()
    {
        if (Vector3.Distance(gameObject.transform.position, PlayerMovement.instance.transform.position) < DistanceToRender && targetManager.instance.VisibleFromCamera(transform.GetChild(1).gameObject))
        {
            Vector3 cameraOffset = Camera.main.transform.position - transform.position;
            otherCam.GetComponent<Camera>().targetTexture = tempTexture;
            otherCam.transform.localPosition = cameraOffset;
            otherCam.transform.localRotation = Quaternion.LookRotation(MouseLook.instance.gameObject.transform.forward, Vector3.up);
            transform.GetChild(1).gameObject.GetComponent<MeshRenderer>().material.mainTexture = tempTexture;
        }
        else
        {
            otherCam.transform.localRotation = Quaternion.identity;
            otherCam.transform.localPosition = Vector3.zero;
        }
       
        //if (playerOverlapping && Vector3.Dot(-PlayerMovement.instance.gameObject.GetComponent<Rigidbody>().velocity.normalized, transform.forward.normalized) > 0)
        //{
        //    Vector3 offset = PlayerMovement.instance.transform.position - transform.position;
        //    float rotationDiff = -Quaternion.Angle(transform.rotation, otherPortal.transform.rotation);
        //    rotationDiff += 180;
        //    PlayerMovement.instance.transform.Rotate(Vector3.up, rotationDiff);
        //    PlayerMovement.instance.gameObject.GetComponent<Rigidbody>().velocity = Vector3.Scale(PlayerMovement.instance.gameObject.GetComponent<Rigidbody>().velocity, otherPortal.transform.forward);

        //    Vector3 positionOffset = Quaternion.Euler(0f, rotationDiff, 0f) * offset;
        //    PlayerMovement.instance.transform.position = otherPortal.transform.position + positionOffset;

        //}
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<MouseLook>() != null)
        {
            playerOverlapping = true;

        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.GetComponent<MouseLook>() != null && Vector3.Dot(-PlayerMovement.instance.gameObject.GetComponent<Rigidbody>().velocity.normalized, transform.forward.normalized) > 0)
        {
            Vector3 offset = PlayerMovement.instance.transform.position - transform.position;
            float rotationDiff = -Quaternion.Angle(transform.rotation, otherPortal.transform.rotation);
            rotationDiff += 180;
            PlayerMovement.instance.transform.Rotate(Vector3.up, rotationDiff);
            PlayerMovement.instance.gameObject.GetComponent<Rigidbody>().velocity = Vector3.Scale(PlayerMovement.instance.gameObject.GetComponent<Rigidbody>().velocity, otherPortal.transform.forward);

            Vector3 positionOffset = Quaternion.Euler(0f, rotationDiff, 0f) * offset;
            PlayerMovement.instance.transform.position = otherPortal.transform.position + positionOffset;

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<MouseLook>() != null)
        {
            playerOverlapping = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, DistanceToRender);
    }
}
