using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NarratorScript : MonoBehaviour
{
    public static NarratorScript instance;

    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private Image backgroundImage;


    private AudioSource sourceRef;
    private int index;
    private Dialogue dialoguesref;
    List<string> messages = new List<string>();
    private float fadeDialogue = 1f;
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
        StartCoroutine(FadeBackgroundImage(true));

        while (index < messages.Count)
        {
            text.text = messages[index];
            sourceRef.clip = dialoguesref.voice;
            sourceRef.Play();
            if(dialoguesref.dialogueEN[index].duration == 0)
            {
                yield return new WaitForSeconds(dialoguesref.voice.length);
            }
            else
            {
                yield return new WaitForSeconds(dialoguesref.dialogueEN[index].duration);
            }
            index++;
        }
        StartCoroutine(FadeBackgroundImage(false));
    }

    private IEnumerator FadeBackgroundImage(bool fadeIn)
    {
        Color startTextColor = text.color;
        Color targetTextColor = new Color(startTextColor.r, startTextColor.g, startTextColor.b, fadeIn ? 1f : 0f);
        Color startBGColor = backgroundImage.color;
        Color targetBGColor = new Color(startBGColor.r, startBGColor.g, startBGColor.b, fadeIn ? 0.6f : 0f);

        float elapsedTime = 0f;

        while (elapsedTime < (fadeDialogue / 10))
        {
            text.color = Color.Lerp(startTextColor, targetTextColor, elapsedTime / (fadeDialogue/10));
            backgroundImage.color = Color.Lerp(startBGColor, targetBGColor, elapsedTime / (fadeDialogue / 10));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        backgroundImage.color = targetBGColor;
        text.color = targetTextColor;

        if(!fadeIn)
        {
            text.text = "";
        }

    }
}