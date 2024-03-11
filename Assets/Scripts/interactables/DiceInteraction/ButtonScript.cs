using UnityEngine;

public class ButtonScript : MonoBehaviour, IInteractable
{

    [SerializeField] private bool canInteract;

    public bool CanInteract()
    {
        return canInteract;
    }
    public void SetInteractable(bool _interactable)
    {
        canInteract = _interactable;
    }


    public void Interact()
    {
       PlayerHUDScript.instance.RollDice(4, this);
    }
    public void GetDiceResult(int result)
    {
      if(result >= 4)
        {
            print("bienvenue");
        }
    }



}
