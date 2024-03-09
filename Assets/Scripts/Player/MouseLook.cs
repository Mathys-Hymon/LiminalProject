using UnityEngine;


public class MouseLook : MonoBehaviour
{
    public static MouseLook instance;


    [SerializeField] private float sensitivity = 10f, xClamp = 85f;
    private float xRotation;
    private Vector2 input;

    private void Start()
    {
        instance = this;
    }

    public void Interact()
    {
        print("interact");
    }

    public void ReceiveInput(Vector2 mouseInput)
    {
        input = mouseInput * sensitivity;
    }

    private void Update()
    {
        Ray ray = new Ray(transform.position + transform.forward, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 2.5f))
        {
            IInteractable interactableObject = hit.collider.GetComponent<IInteractable>();
            if (interactableObject != null)
            {
                PlayerHUDScript.instance.SetCrosshairVisible(true);
            }
            else
            {
                PlayerHUDScript.instance.SetCrosshairVisible(false);
            }
        }
        else
        {
            PlayerHUDScript.instance.SetCrosshairVisible(false);
        }

        xRotation -= ((input.y / 10) * sensitivity);
        xRotation = Mathf.Clamp(xRotation, -xClamp, xClamp);
        var VerticalRot = Quaternion.AngleAxis(xRotation, Vector3.right);
        transform.localRotation = VerticalRot;

        PlayerMovement.instance.transform.Rotate((Vector3.up * (input.x / 10) * sensitivity));
    }
}
