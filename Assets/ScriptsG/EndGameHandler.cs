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
    InputAction instantEnd;
    InputAction restart;

    private void Start()
    {
        instantEnd = InputSystem.actions.FindAction("InstantEnd");
        restart = InputSystem.actions.FindAction("Restart");
    }
    /// <summary>
    /// Adds to your total hard evidence
    /// </summary>
    public void CollectHardEvidence()
    {
        totalHardEvidence += 1;
        if(totalHardEvidence == 4)
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
        endText.text = "You collected " + totalHardEvidence + " pieces of evidence";
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
