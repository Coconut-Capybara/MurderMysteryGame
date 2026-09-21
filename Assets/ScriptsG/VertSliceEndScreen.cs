using UnityEngine;
using UnityEngine.SceneManagement;

public class VertSliceEndScreen : MonoBehaviour
{

    public GameObject vertSlicePanel;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ResetGame()
    {
        SceneManager.LoadScene("TempMainMenu");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
