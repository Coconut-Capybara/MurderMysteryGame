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
    public void CollectHardEvidence()
    {
        totalHardEvidence += 1;
        if(totalHardEvidence == 4)
        {
            EndGame();      
        }
    }
    public void EndGame()
    {
        GameObject.FindFirstObjectByType<PlayerInteract>().inGame = false;
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
