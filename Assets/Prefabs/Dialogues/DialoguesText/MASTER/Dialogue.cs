using UnityEngine;

[CreateAssetMenu(fileName = "Dialogue", menuName = "new Dialogue")]
public class Dialogue : ScriptableObject
{
    [System.Serializable]
    public class dialogue
    {
        public string text;
        public float duration;
    }
    public dialogue[] dialogueEN;
    public dialogue[] dialogueFR;
    public bool playerCanInteract;
    public AudioClip voice;
}
