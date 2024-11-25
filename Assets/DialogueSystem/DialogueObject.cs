using UnityEngine;

[CreateAssetMenu(fileName = "Dialogue", menuName = "Dialogue System/Dialogue", order = 1)]
public class DialogueSystem : ScriptableObject
{
    public Dialogue[] dialogueLines;
}
