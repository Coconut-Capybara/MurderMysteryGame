/*****************************************************************************
// Script Name : EndGameHandler
// Author : Gabriel Andrews
// Additional Author(s) :
// Creation Date:10/02/26
// Last Modified Date: 10/14/26
//
// Summary : Handles the hard evidence, and end game state
*****************************************************************************/
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndGameHandler : MonoBehaviour
{
    [SerializeField] private int totalHardEvidence;
    [SerializeField] private GameObject endGamePanel;
    [SerializeField] private TMP_Text endText;
    [SerializeField] private string[] endings;
    [SerializeField] private TMP_Text evidenceNum;
    [SerializeField] private Image[] journalImages;
    [SerializeField] private GameObject[] journalDescs;
    [SerializeField] private BGController bgController;
    [SerializeField] private float textSpeed;
    [SerializeField] private GameObject menuButton;
    [SerializeField] private TMP_Text endTimeText;
    InputAction instantEnd;
    InputAction restart;
    private int timeRemaining;

    private void Start()
    {
        instantEnd = InputSystem.actions.FindAction("InstantEnd");
        restart = InputSystem.actions.FindAction("Restart");
        //evidenceNum.text = "Evidence Collected: " + totalHardEvidence;
    }
    /// <summary>
    /// Adds to your total hard evidence
    /// </summary>
    public void CollectHardEvidence()
    {
        totalHardEvidence += 1;
        //evidenceNum.text = "Evidence Collected: " + totalHardEvidence;
        if (totalHardEvidence == 4)
        {
            EndGame();
        }
    }
    public void UpdateJournal(GameObject g)
    {
        journalImages[totalHardEvidence].sprite = g.GetComponent<ObjectProperties>().hardEvidenceSprite;
        journalImages[totalHardEvidence].gameObject.SetActive(true);
        journalDescs[totalHardEvidence].GetComponent<TMP_Text>().text = g.GetComponent<ObjectProperties>().hardEvidenceDesc;
    }
    /// <summary>
    /// Changes the ending based on number of evidence
    /// </summary>
    public void EndGame()
    {
        GameObject.FindFirstObjectByType<PlayerInteract>().SetInGame(false);    
        GameObject.FindFirstObjectByType<PlayerInteract>().HidePrompt();
        GameObject.FindFirstObjectByType<PlayerInteract>().cursorState = PlayerInteract.CursorState.None;
        endGamePanel.SetActive(true);
        bgController.ResultsMusic();
        switch (totalHardEvidence)
        {
            case 0: StartCoroutine(ShowText(endings[0])); break;
            case 1: StartCoroutine(ShowText(endings[1])); break;
            case 2: StartCoroutine(ShowText(endings[2])); break;
            case 3: StartCoroutine(ShowText(endings[3])); break;
            case 4: StartCoroutine(ShowText(endings[4])); break;
        }
    }
    private IEnumerator ShowText(string _prompt)
    {
        int i = 0;
        endText.text = "";
        while (i < _prompt.Length)
        {
            endText.text += _prompt[i];
            i++;
            yield return new WaitForSeconds(textSpeed);
        }
        if(TimeController.timeLeft >=0)
        {
            endTimeText.text = "You had " + TimeController.timeLeft.ToString() + " minutes left";
        }
        else
        {
            timeRemaining = (TimeController.timeLeft * -1);
            endTimeText.text = "You went over by " + timeRemaining.ToString() + " minutes";
        }

            endTimeText.gameObject.SetActive(true); 
        menuButton.SetActive(true);
    }
    public void ExitToMenu()
    {
        SceneManager.LoadScene(0);
    }
    private void Update()
    {
        if (instantEnd.WasPressedThisFrame())
        {
            EndGame();
        }
        if(restart.WasPressedThisFrame())
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
