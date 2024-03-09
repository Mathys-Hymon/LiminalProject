using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUDScript : MonoBehaviour
{
    [SerializeField] private RawImage crosshair;

    public static PlayerHUDScript instance;

    void Start()
    {
        instance = this;
    }

    public void SetCrosshairVisible(bool visible)
    {
        float lerpColor = 0;
        if(visible)
        {
            lerpColor = 1;
        }

        crosshair.color = Vector4.Lerp(crosshair.color, new Vector4(1, 1, 1, lerpColor), 20 * Time.deltaTime);
    }
}
