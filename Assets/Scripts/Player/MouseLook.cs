using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public static MouseLook instance;


    [SerializeField] private float sensitivity = 10f, xClamp = 85f;
    private Vector2 rotation;
    private Vector2 input;
    private bool interact;

    private void Start()
    {
        instance = this;
    }

    public void Interact(bool _interact)
    {
        interact = _interact;
    }

    public void ReceiveInput(Vector2 mouseInput)
    {
        input = mouseInput * sensitivity;
    }
    private void Update()
    {
        Ray ray = new Ray(transform.position + (transform.forward/3), transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 2.5f))
        {
            IInteractable interactableObject = hit.collider.GetComponent<IInteractable>();
            if (interactableObject != null && interactableObject.CanInteract() == true)
            {
                PlayerHUDScript.instance.SetCrosshairVisible(true);
                if(interact)
                {
                    interact = false;
                    interactableObject.Interact();
                }
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

        rotation -= new Vector2((input.y * sensitivity * Time.deltaTime), (input.x * sensitivity * Time.deltaTime));
        rotation.x = Mathf.Clamp(rotation.x, -xClamp, xClamp);
        transform.localRotation = Quaternion.Euler(rotation.x, -rotation.y, 0);
    }
}
