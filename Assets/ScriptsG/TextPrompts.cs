/*****************************************************************************
// Script Name : TextPrompt
// Author : Gabriel Andrews
// Additional Author(s) :
// Creation Date:9/19/26
// Last Modified Date: 9/19/26
//
// Summary : throws up a given prompt after an interaction
*****************************************************************************/
using TMPro;
using UnityEngine;

public class TextPrompts : MonoBehaviour
{
    [SerializeField] private GameObject promptPanel;
    [SerializeField] private TMP_Text promptText;
    /// <summary>
    /// Throws up prompt
    /// </summary>
    /// <param name="_prompt"></param>
    public void ShowPrompt(string _prompt)
    {
       promptPanel.SetActive(true);
       promptText.text = _prompt;    
       GameObject.FindFirstObjectByType<PlayerInteract>().inPrompt = true;
       GameObject.FindFirstObjectByType<PlayerInteract>().cursorState = PlayerInteract.CursorState.None;
        
        if(GameObject.FindAnyObjectByType<RockScript>()  != null)
        {
            GameObject.FindAnyObjectByType<RockScript>().gameObject.GetComponent<ObjectProperties>().givesPrompt = false;
        }
    }

    public void HidePrompt()
    {
        promptPanel.SetActive(false);
        GameObject.FindFirstObjectByType<PlayerInteract>().inPrompt = false;
        promptText.text = " ";
    }
}
