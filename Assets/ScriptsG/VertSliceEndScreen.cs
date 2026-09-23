/*****************************************************************************
// Script Name : VertSliceEndScreen
// Author : Bryson Welch
// Additional Author(s) :
// Creation Date: 9/21/26
// Last Modified Date: 9/21/26
//
// Summary : Deals with the Vertical Slice End Screen
*****************************************************************************/
using UnityEngine;
using UnityEngine.SceneManagement;

public class VertSliceEndScreen : MonoBehaviour
{
    public GameObject vertSlicePanel;
    /// <summary>
    /// Quits the game
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
    }
    /// <summary>
    /// Sends you to main menu which resets all puzzle progress
    /// </summary>
    public void ResetGame()
    {
        SceneManager.LoadScene("TempMainMenu");
    }
}
