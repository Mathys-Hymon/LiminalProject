using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [Header("Dialogues")]
    [SerializeField] Dialogue dialogue;


    [Header("Exit data")]
    [SerializeField] bool inBoxDialogue;
    [SerializeField] string[] exitTextBasedOnDuration;
    [SerializeField] float[] duration;

    private bool alreallyTriggered;
    private float delay;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player" && !alreallyTriggered)
        {
            NarratorScript.instance.Dialogue(dialogue);
            alreallyTriggered = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(inBoxDialogue)
        {
            delay += Time.deltaTime;
        }
    }

}
