/*****************************************************************************
// Script Name : EndGameHandler
// Author : Gabriel Andrews
// Additional Author(s) :
// Creation Date:10/02/26
// Last Modified Date: 10/14/26
//
// Summary : Handles the hard evidence, and end game state
*****************************************************************************/
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class EndGameHandler : MonoBehaviour
{
    [SerializeField] private int totalHardEvidence;
    [SerializeField] private GameObject endGamePanel;
    [SerializeField] private TMP_Text endText;
    [SerializeField] private string[] endings;
    [SerializeField] private TMP_Text evidenceNum;
    InputAction instantEnd;
    InputAction restart;

    private void Start()
    {
        instantEnd = InputSystem.actions.FindAction("InstantEnd");
        restart = InputSystem.actions.FindAction("Restart");
        evidenceNum.text = "Evidence Collected: " + totalHardEvidence;
    }
    /// <summary>
    /// Adds to your total hard evidence
    /// </summary>
    public void CollectHardEvidence()
    {
        totalHardEvidence += 1;
        evidenceNum.text = "Evidence Collected: " + totalHardEvidence;
        if (totalHardEvidence == 4)
        {
            EndGame();      
        }
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
        switch (totalHardEvidence)
        {
            case 0: endText.text = endings[0]; break;
            case 1: endText.text = endings[1]; break;
            case 2: endText.text = endings[2]; break;
            case 3: endText.text = endings[3]; break;
            case 4: endText.text = endings[4]; break;
        }
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
