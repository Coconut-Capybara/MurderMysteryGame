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
            textCrawlTextBox.text += text[i];
            i++;
            yield return new WaitForSeconds(textSpeed);
        }
    }
}
