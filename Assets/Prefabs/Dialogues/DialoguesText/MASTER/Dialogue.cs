using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialogue", menuName = "new Dialogue")]
public class Dialogue : ScriptableObject
{
    public string dialogueEN;
    public string dialogueFR;
    public AudioClip voice;
    public float delay;
}
