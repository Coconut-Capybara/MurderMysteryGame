using System.Collections;
using TMPro;
using UnityEngine;

public class OpeningTextCrawl : MonoBehaviour
{
    [SerializeField] private GameObject openingPanel;
    [SerializeField] private PlayerInteract playerInteract;
    [SerializeField] private TMP_Text textCrawlTextBox;
    [SerializeField] private float textSpeed;
    [SerializeField] private string textCrawlString;
    [SerializeField] private float endSentenceWaitTime;
    [SerializeField] private float commaWaitTime;
    private float codeTextSpeed;
    [SerializeField] private int[] enterCharacter;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        openingPanel.SetActive(true);
        playerInteract.SetInGame(false);
        playerInteract.SetInPrompt(true);
        StartCoroutine(ShowText(textCrawlString));
    }


    public void CloseOpening()
    {
        openingPanel.SetActive(false);
        playerInteract.SetInGame(true);
        playerInteract.SetInPrompt(false);
    }

    private IEnumerator ShowText(string text)
    {
        int i = 0;
        while (i < text.Length)
        {
            if (text[i].ToString() == ".")
            {
                codeTextSpeed = endSentenceWaitTime;
            }
            else if (text[i].ToString() == ",")
            {
                codeTextSpeed = commaWaitTime;
            }
            else
            {
                codeTextSpeed = textSpeed;
            }
            if (text[i].ToString() == "-")
            {
                textCrawlTextBox.text += "\n";
            }
            for(int o = 0;  o < enterCharacter.Length; o++)
            {
                if (i == enterCharacter[o])
                {
                    textCrawlTextBox.text += "\n";
                }
            }

            textCrawlTextBox.text += text[i];
            i++;
            yield return new WaitForSeconds(codeTextSpeed);
        }
    }
}
