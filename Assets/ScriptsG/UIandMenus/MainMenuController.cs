using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private string firstScene;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void StartButton()
    {
        SceneManager.LoadScene(firstScene);
    }

    public void QuitButton()
    {
        Application.Quit();
    }
}
