using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NarratorScript : MonoBehaviour
{
    public static NarratorScript instance;

    [SerializeField] private TextMeshProUGUI text;
    private AudioSource sourceRef;
    private int dialogueIndex;
    private float delay;
    private string displayedDialogue;

    private float[] dialogueDelay;
    private string[] message;
    private AudioClip[] voice;
    private float typingSpeed;
    private void Start()
    {
        instance = this;

        sourceRef = GetComponent<AudioSource>();
    }

    public void Dialogue(string[] _message, AudioClip[] _voice, float[] _delay)
    {
        dialogueIndex = 0;
        message = _message;
        voice = _voice;
        dialogueDelay = _delay;
    }

    private void Update()
    {
        if(message != null)
        {
            if (!sourceRef.isPlaying && dialogueIndex <= message.Length - 1)
            {
                if (delay < dialogueDelay[dialogueIndex])
                {
                    delay += Time.deltaTime;
                }
                else
                {
                    typingSpeed = voice[dialogueIndex].length / message[dialogueIndex].Length;
                    print(message[dialogueIndex]);
                    delay = 0;

                    StartCoroutine(DisplayDialogue(message[dialogueIndex]));
                    sourceRef.clip = voice[dialogueIndex];
                    sourceRef.Play();
                    dialogueIndex++;
                }

            }
            else if (dialogueIndex > message.Length - 1)
            {
                message = null;
                dialogueIndex = 0;
            }
        }
        
    }

    private IEnumerator DisplayDialogue(string line)
    {
        text.text = "";

        foreach(char letter in line.ToCharArray())
        {
            text.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    public void StopDialogue()
    {

    }
}
