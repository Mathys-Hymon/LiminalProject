using UnityEngine;


public class portalScript : MonoBehaviour
{
    [SerializeField] private portalScript otherPortal;
    [SerializeField] private float DistanceToRender;

    private Camera cam;
    private Camera otherCam;

    private RenderTexture tempTexture;

    private bool playerOverlapping;


    public Camera GetCam()
    {
        return cam;
    }

    private void Awake()
    {
        cam = transform.GetChild(0).gameObject.GetComponent<Camera>();
        cam.fieldOfView = Camera.main.fieldOfView;
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
            otherCam.targetTexture = tempTexture;
            otherCam.gameObject.transform.localPosition = cameraOffset;
            otherCam.gameObject.transform.localRotation = Quaternion.LookRotation(MouseLook.instance.gameObject.transform.forward, Vector3.up);
            transform.GetChild(1).gameObject.GetComponent<MeshRenderer>().material.mainTexture = tempTexture;
            PortalOffset(Camera.main);
            otherPortal.PortalOffset(otherCam);
        }
        else
        {
            otherCam.gameObject.transform.localRotation = Quaternion.identity;
            otherCam.gameObject.transform.localPosition = Vector3.zero;
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

    public void PortalOffset(Camera _cam)
    {
        float halfHeight = _cam.nearClipPlane * Mathf.Tan(_cam.fieldOfView * 0.5f * Mathf.Deg2Rad);
        float halfWidth = halfHeight * _cam.aspect;
        float dstToNearClipPlaneCorner = new Vector3(halfWidth, halfHeight, _cam.nearClipPlane).magnitude;

        Transform screenT = transform.GetChild(1).transform;
        bool camFacingSameDirAsPortal = Vector3.Dot(transform.forward, transform.position - _cam.transform.position) > 0;
        screenT.localScale = new Vector3(screenT.localScale.x, screenT.localScale.y, dstToNearClipPlaneCorner);
        screenT.localPosition = Vector3.forward * dstToNearClipPlaneCorner * ((camFacingSameDirAsPortal) ? 0.5f : -0.5f);
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

            // Calculer la nouvelle direction de la vitesse
            Vector3 newVelocityDirection = Quaternion.AngleAxis(rotationDiff, Vector3.up) * PlayerMovement.instance.gameObject.GetComponent<Rigidbody>().velocity.normalized;

            // Ajouter la nouvelle vitesse à la vitesse actuelle du joueur
            PlayerMovement.instance.gameObject.GetComponent<Rigidbody>().velocity = newVelocityDirection * PlayerMovement.instance.gameObject.GetComponent<Rigidbody>().velocity.magnitude;

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
