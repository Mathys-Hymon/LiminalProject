using UnityEngine;

public class targetManager : MonoBehaviour
{
    public static targetManager instance;

    private Camera cam;

    private void Start()
    {
        instance = this;
        cam = Camera.main;

    }

    public bool VisibleFromCamera(GameObject target)
    {
        Plane[] frustumPlanes = GeometryUtility.CalculateFrustumPlanes(cam);
        return GeometryUtility.TestPlanesAABB(frustumPlanes, target.GetComponent<Collider>().bounds);
    }
}
