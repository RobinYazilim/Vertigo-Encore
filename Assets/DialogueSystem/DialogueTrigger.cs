using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public DialogueSystem[] dialogue;
    public bool countable = false;
    public int count = 0;

    public DialogueSystem GetDialogue()
    {
        DialogueSystem targetDialogue = dialogue[count];
        if (countable)
        {
            if (count + 1  < dialogue.Length)
            {
                count++;
            }
        }
        return targetDialogue;

    }
}
