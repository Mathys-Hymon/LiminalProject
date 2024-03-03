using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] private string[] dialogues;
    [SerializeField] private AudioClip[] audioClip;
    [SerializeField] private float[] textDelay;
    [SerializeField] private bool inboxDialogue;

    private bool alreallyTriggered;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player" && !alreallyTriggered)
        {
            NarratorScript.instance.Dialogue(dialogues, audioClip, textDelay);
            alreallyTriggered = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(inboxDialogue)
        {
            NarratorScript.instance.StopDialogue();
        }
    }
}
