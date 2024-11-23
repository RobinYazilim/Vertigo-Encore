using UnityEngine;

[System.Serializable]
public class Dialogue
{
    public string speakerName;
    [TextArea(3, 10)]
    public string dialogueText;
    public Sprite characterPhoto;
    public AudioClip letterSound;

    public override string ToString()
    {
        return $"Speaker: {speakerName}, Text: {dialogueText}, Photo: {(characterPhoto != null ? characterPhoto.name : "None")}, Sound: {(letterSound != null ? letterSound.name : "None")}";
    }
}
