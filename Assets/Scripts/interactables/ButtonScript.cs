using UnityEngine;

public class ButtonScript : MonoBehaviour, IInteractable
{
    [SerializeField] private string _prompt;
    public string InteractionPrompt => _prompt;
    public void Interact()
    {
        print("click"); 
    }
}
