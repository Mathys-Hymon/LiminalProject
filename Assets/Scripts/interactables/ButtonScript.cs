using UnityEngine;

public class ButtonScript : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        print("click"); 

        if(PlayerHUDScript.instance.RollDice() >= 1)
        {

        }
    }
}
