using UnityEngine;

public interface IInteractable
{
    public void Interact();
    public bool CanInteract();
    public void SetInteractable(bool _interactable);
    public void GetDiceResult(int result);
}
