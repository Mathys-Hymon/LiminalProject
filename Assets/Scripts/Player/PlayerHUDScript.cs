using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUDScript : MonoBehaviour
{
    [SerializeField] private GameObject crosshairGo;
    [SerializeField] private RawImage crosshair;

    private bool roll;

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

    public int RollDice()
    {
            
        roll = true;
        return 0;
    }

    private void Update()
    {
        if(roll)
        {
            crosshair.transform.localScale = Vector3.Lerp(crosshair.transform.localScale, new Vector3(0.5f, 0.5f, 0.5f), 6 * Time.deltaTime);
        }
        else
        {
            crosshair.transform.localScale = Vector3.Lerp(crosshair.transform.localScale, new Vector3(0.06f, 0.06f, 0.06f), 3 * Time.deltaTime);
        }
    }
}
