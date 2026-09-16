using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class SceneSwitchDebug : MonoBehaviour
{
    InputAction nextScene;
    public int sceneIndex;
    public string anotherScene;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        nextScene = InputSystem.actions.FindAction("NextScene");
    }

    // Update is called once per frame
    void Update()
    {
        if (nextScene.WasPressedThisFrame())
        {
            SceneManager.LoadScene(anotherScene);
        }
    }
}
