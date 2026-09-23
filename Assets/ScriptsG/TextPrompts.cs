/*****************************************************************************
// Script Name : TextPrompt
// Author : Gabriel Andrews
// Additional Author(s) :
// Creation Date:9/19/26
// Last Modified Date: 9/19/26
//
// Summary : throws up a given prompt after an interaction
*****************************************************************************/
using System.Collections;
using TMPro;
using UnityEngine;

public class TextPrompts : MonoBehaviour
{
    [SerializeField] private GameObject promptPanel;
    [SerializeField] private GameObject continueButton;
    [SerializeField] private TMP_Text promptText;
    [SerializeField] private float textSpeed;
    private Coroutine promptTextCO;
    /// <summary>
    /// Throws up prompt
    /// </summary>
    /// <param name="_prompt"></param>
    public void ShowPrompt(string _prompt)
    {
       promptPanel.SetActive(true); 
       GameObject.FindFirstObjectByType<PlayerInteract>().inPrompt = true;
       GameObject.FindFirstObjectByType<PlayerInteract>().cursorState = PlayerInteract.CursorState.None;
        if (GameObject.FindFirstObjectByType<PlayerInteract>().inPrompt)
        {
            promptTextCO = StartCoroutine(ShowText(_prompt));
        }
    }
    private IEnumerator ShowText(string _prompt)
    {
        int i = 0;
        while (i < _prompt.Length)
        {
            promptText.text += _prompt[i];
            i++;
            yield return new WaitForSeconds(textSpeed);
        }
        continueButton.SetActive(true);
    }
    public void HidePrompt()
    {
        promptPanel.SetActive(false);
        continueButton.SetActive(false);
        if(promptTextCO != null)
        {
            StopCoroutine(promptTextCO);        
        }
        GameObject.FindFirstObjectByType<PlayerInteract>().inPrompt = false;
        promptText.text = " ";
    }
}
