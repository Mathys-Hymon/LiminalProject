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
    private float delay, typingSpeed;
    List<string> messages = new List<string>();

    private Dialogue[] dialoguesref;
    private void Start()
    {
        instance = this;

        sourceRef = GetComponent<AudioSource>();
    }

    public void Dialogue(Dialogue[] _dialogue)
    {
        dialogueIndex = 0;
        dialoguesref = _dialogue;

        switch (PlayerPrefs.GetInt("language"))
        {
            default:
                for (int i = 0; i < dialoguesref.Length; i++)
                {
                    messages.Add(dialoguesref[i].dialogueEN);
                }
                break;


            case 0: //anglais
                for (int i = 0; i < dialoguesref.Length; i++)
                {
                    messages.Add(dialoguesref[i].dialogueEN);
                }
                break;

            case 1: //francais
                for (int i = 0; i < dialoguesref.Length; i++)
                {
                    messages.Add(dialoguesref[i].dialogueFR);
                }
                break;
        }
    }

    private void Update()
    {
        if(dialoguesref != null)
        {
            if (!sourceRef.isPlaying && dialogueIndex <= messages.Count - 1)
            {
                if (delay < dialoguesref[dialogueIndex].delay)
                {
                    delay += Time.deltaTime;
                }
                else
                {
                    typingSpeed = dialoguesref[dialogueIndex].voice.length / messages[dialogueIndex].Length;
                    print(messages[dialogueIndex]);
                    delay = 0;

                    StartCoroutine(DisplayDialogue(messages[dialogueIndex]));
                    sourceRef.clip = dialoguesref[dialogueIndex].voice;
                    sourceRef.Play();
                    dialogueIndex++;
                }

            }
            else if (dialogueIndex > messages.Count - 1)
            {
                dialoguesref = null;
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

    public void StopDialogue(string line)
    {

    }
}
