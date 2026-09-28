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

public class PauseMenu : MonoBehaviour
{
    InputAction quit;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        quit = InputSystem.actions.FindAction("Quit");
    }

    // Update is called once per frame
    void Update()
    {
        if (quit.WasPressedThisFrame())
        {
            Application.Quit();
        }
    }
}
