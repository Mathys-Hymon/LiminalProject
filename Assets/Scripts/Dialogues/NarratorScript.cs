using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NarratorScript : MonoBehaviour
{
    public static NarratorScript instance;

    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private GameObject backgroundImage;
    private AudioSource sourceRef;
    private int index;
    private Dialogue dialoguesref;
    List<string> messages = new List<string>();
    private void Start()
    {
        instance = this;
        sourceRef = GetComponent<AudioSource>();
    }

    public void Dialogue(Dialogue _dialogue)
    {
        StopAllCoroutines();

        index = 0;
        dialoguesref = _dialogue;
        messages.Clear();
        sourceRef.Stop();

        switch (PlayerPrefs.GetInt("language"))
        {
            case 0: //anglais
                for (int i = 0; i < dialoguesref.dialogueEN.Length; i++)
                {
                    messages.Add(dialoguesref.dialogueEN[i].text);
                }
                break;

            case 1: //francais
                for (int i = 0; i < dialoguesref.dialogueFR.Length; i++)
                {
                    messages.Add(dialoguesref.dialogueFR[i].text);
                }
                break;
        }

        StartCoroutine(ShowDialogue());
    }

    private IEnumerator ShowDialogue()
    {
        sourceRef.clip = dialoguesref.voice;
        sourceRef.Play();
        while (index < messages.Count)
        {
            backgroundImage.GetComponent<Image>().color = new Vector4(0, 0, 0, 0.3f);
            text.text = messages[index];


            float currentDuration = dialoguesref.dialogueEN[index].duration;

            yield return new WaitForSeconds(currentDuration);

            index++;
        }

        text.text = "";
        backgroundImage.GetComponent<Image>().color = new Vector4(0, 0, 0, 0);
    }
}