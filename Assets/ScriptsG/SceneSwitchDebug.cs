using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class SceneSwitchDebug : MonoBehaviour
{
    InputAction nextScene;
    InputAction quit;
    public int sceneIndex;
    public string anotherScene;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        nextScene = InputSystem.actions.FindAction("NextScene");
        quit = InputSystem.actions.FindAction("Quit");
    }

    // Update is called once per frame
    void Update()
    {
        if (nextScene.WasPressedThisFrame())
        {
            SceneManager.LoadScene(anotherScene);
        }
        if (quit.WasPressedThisFrame())
        {
            Application.Quit();
        }
        
    }
}
