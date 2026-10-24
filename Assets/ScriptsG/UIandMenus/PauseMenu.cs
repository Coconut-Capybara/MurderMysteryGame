/*****************************************************************************
// Script Name : PauseMenu
// Author : bryson Welch
// Additional Author(s) :
// Creation Date: 9/21/26
// Last Modified Date: 9/21/26
//
// Summary : Currently acts as the quit button as of Vertical Slice
*****************************************************************************/
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    InputAction pause;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject youSurePanel;
    [SerializeField] private PlayerInteract playerInteract;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pause = InputSystem.actions.FindAction("Pause");
        youSurePanel.SetActive(false);
        pausePanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (pause.WasPressedThisFrame() && 
            (pausePanel.activeInHierarchy || youSurePanel.activeInHierarchy))
        {
            ClosePauseMenu();
        }
        else if (pause.WasPressedThisFrame())
        {
            PauseMenuFunction();
        }
    }

    public void ClosePauseMenu()
    {
        pausePanel.SetActive(false);
        youSurePanel.SetActive(false);
        playerInteract.inGame = true;
    }

    private void PauseMenuFunction()
    {

        if(pausePanel.activeInHierarchy == false)
        {
            pausePanel.SetActive(true);
            playerInteract.inGame = false;
        }

        //pausePanel.gameObject.SetActive(!pausePanel.activeInHierarchy);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Resume()
    {
        ClosePauseMenu();
    }

    public void QuitToMenu()
    {
        pausePanel.SetActive(false);
        DoubleCheck();
    }

    private void DoubleCheck()
    {
        youSurePanel.SetActive(true);
    }

    public void GoBack()
    {
        youSurePanel.SetActive(false);
    }

    public void GoForward()
    {
        SceneManager.LoadScene("TempMainMenu");
    }
}
