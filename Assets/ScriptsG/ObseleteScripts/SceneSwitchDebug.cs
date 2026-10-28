/*****************************************************************************
// Script Name : SceneSwitchDebug
// Author : Bryson Welch
// Additional Author(s) :
// Creation Date:9/15/26
// Last Modified Date: 9/15/26
//
// Summary : Lets the player move to the specified scene in editor by Using the 'l' key
*****************************************************************************/
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class SceneSwitchDebug : MonoBehaviour
{
    InputAction nextScene;
    InputAction quit;
    [Tooltip("The name of the scene to go to")]
    public string anotherScene;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        nextScene = InputSystem.actions.FindAction("NextScene");
        quit = InputSystem.actions.FindAction("Quit");
    }

    /// <summary>
    /// Checks for inputs and performs the corresponding action
    /// </summary>
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
