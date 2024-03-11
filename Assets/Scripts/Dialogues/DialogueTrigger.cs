using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [Header("Dialogues")]
    [SerializeField] Dialogue dialogue;
    [SerializeField] GameObject eventToTrigger;
    [SerializeField] bool triggerEndDialogue, changePlayerInteraction, newPlayerInteraction;

    [Header("Exit data")]
    [SerializeField] bool inBoxDialogue;
    [SerializeField] Dialogue[] exitTextBasedOnDuration;
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
        if(triggerEndDialogue && eventToTrigger != null)
        {
            Invoke(nameof(TriggerEvent), dialogue.voice.length + 1);
        }
        else if (eventToTrigger != null && !triggerEndDialogue)
        {
            TriggerEvent();
        }
    }

    private void TriggerEvent()
    {
        eventToTrigger.GetComponent<IInteractable>().Interact();
        if(changePlayerInteraction)
        {
            eventToTrigger.GetComponent<IInteractable>().SetInteractable(newPlayerInteraction);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(inBoxDialogue)
        {
            delay += Time.deltaTime;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(inBoxDialogue)
        {
            for (int i = 0; i < duration.Length; i++)
            {
                if (delay > duration[i] && delay < duration[i + 1])
                {
                    NarratorScript.instance.Dialogue(exitTextBasedOnDuration[i]);
                }
            }
        } 
    }
}
