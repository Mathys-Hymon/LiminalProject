using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    public bool IsOnCamera(GameObject target)
    {
        if (GeometryUtility.TestPlanesAABB(GeometryUtility.CalculateFrustumPlanes(cam), target.GetComponent<Collider>().bounds))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
