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

    public GameObject dialogueUI;


    private InputAction skipAction;
    private bool stopTyping;
    private bool isTyping;
    private bool onLast;
    Mover currentScript;

    public void StartDialogue(DialogueSystem dialogue)
    {
        currentDialogue = dialogue;
        currentLineIndex = 0;
        stopTyping = false;
        isTyping = false;
        onLast = false;
        dialogueUI.SetActive(true);

        currentScript = FindFirstObjectByType<Mover>();
        currentScript.isAvailable = false;

        DisplayLine();
    }

    public void DisplayLine()
    {
        if (currentDialogue == null || currentDialogue.dialogueLines.Length == currentLineIndex)
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
        if (currentDialogue.dialogueLines.Length == currentLineIndex)
        {
            onLast = true;
        }
        DisplayLine();
    }

    private void EndDialogue()
    {
        
        Debug.Log("Dialogue ended!");
        dialogueUI.SetActive(false);
        currentDialogue = null;
        currentScript.isAvailable = true;
        currentScript = null;
    }

    IEnumerator TypeDialogue(string text)
    {
        if (isTyping) yield break;
        isTyping = true;
        dialogueText.text = "";
        stopTyping = false;

        foreach (char letter in text.ToCharArray())
        {
            if (stopTyping)
            {
                stopTyping = false;
                break;
            }
            dialogueText.text += letter;
            letterSound.Play();
            yield return new WaitForSeconds(0.1f);
        }
        dialogueText.text = text;
        isTyping = false;
        Debug.Log("Done with typing.");
    }

    void Start()
    {
        var playerInput = GetComponent<PlayerInput>();
        skipAction = playerInput.actions["Dialogue/ProgressDialogue"];
        skipAction.Enable();
        skipAction.performed += OnSkipActionPerformed;
    }
    private void OnSkipActionPerformed(InputAction.CallbackContext context)
    {
        Debug.Log(isTyping);
        if (!dialogueUI.activeSelf)
        {
            return;
        }
        if (!isTyping)
        {
            NextLine();
        }
        else if(onLast)
        {
            Debug.Log("no dialogue rn");
        }
        else
        {
            stopTyping = true;
        }
    }
}
