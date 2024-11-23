using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.InputSystem;
using System;
using TMPro;
using static System.Net.Mime.MediaTypeNames;

public class DialogueManager : MonoBehaviour
{
    public TMP_Text dialogueText;
    public TMP_Text speakerName;
    public UnityEngine.UI.Image characterPhoto;
    public AudioSource letterSound;

    private DialogueSystem currentDialogue;
    private int currentLineIndex;


    private InputAction skipAction;
    private bool stopTyping;

    public void StartDialogue(DialogueSystem dialogue)
    {
        currentDialogue = dialogue;
        currentLineIndex = 0;
        DisplayLine();
    }

    public void DisplayLine()
    {
        if (currentDialogue == null)
        {
            EndDialogue();
            return;
        }

        Dialogue line = currentDialogue.dialogueLines[currentLineIndex];
        Debug.Log(line);

        speakerName.text = line.speakerName;
        //dialogueText.text = line.dialogueText;
        StartCoroutine(TypeDialogue(line.dialogueText));
        
        //characterPhoto.sprite = line.characterPhoto;
    }

    public void NextLine()
    {
        currentLineIndex++;
        DisplayLine();
    }

    private void EndDialogue()
    {
        Debug.Log("Dialogue ended!");
    }

    IEnumerator TypeDialogue(string text)
    {
        dialogueText.text = "";
        foreach (char letter in text.ToCharArray())
        {
            if (stopTyping)
            {
                stopTyping = false;
                break;
            }
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.1f);
            if (skipAction.ReadValue<float>() > 0)
            {
                dialogueText.text = text;
                break;
            }
        }
        while (true)
        {
            if (skipAction.ReadValue<float>() > 0)
            {
                break;
            }
            yield return new WaitForSeconds(0.05f);
        }
        NextLine();
    }

    void Start()
    {
        var playerInput = GetComponent<PlayerInput>();
        skipAction = playerInput.actions["Dialogue/ProgressDialogue"];
    }
}
