using UnityEngine;

public class CopController : MonoBehaviour
{
    [Tooltip("The dialogue (in order) the cop will say")]
    public string[] dialogue;

    [Tooltip("FOR TESTING PURPOSES, PLEASE LEAVE AT 0.")]
    public int dialogueIndex;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        dialogueIndex = 0;
    }

    public void UpdateTimePrompt(ObjectProperties objectProperties)
    {
        objectProperties.resultsText[0] = dialogue[dialogueIndex];
        if(dialogueIndex < dialogue.Length - 1)
        {
            dialogueIndex++;
        }
    }
}
