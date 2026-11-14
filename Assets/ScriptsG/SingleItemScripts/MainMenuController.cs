using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private string firstScene;
    [Tooltip("The time the player will start with outside.")]
    [SerializeField] private int timeStart;
    [SerializeField] private GameObject creditsPanel;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        TimeController.timeLeft = timeStart;
    }

    public void StartButton()
    {
        SceneManager.LoadScene(firstScene);
    }

    public void QuitButton()
    {
        Application.Quit();
    }

    public void AlphaButton()
    {
        SceneManager.LoadScene("AttatractionInterior");
    }

    public void CreditsButton()
    {
        creditsPanel.SetActive(true);
    }

    public void CloseCreditsButton()
    {
        creditsPanel.SetActive(false);
    }
}
