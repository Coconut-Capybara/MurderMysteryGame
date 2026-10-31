/*****************************************************************************
// Script Name : SafeOpenScript
// Author : Gabriel Andrews
// Additional Author(s) :
// Creation Date:9/26/26
// Last Modified Date: 10/14/26
//
// Summary : Handles the safe code
*****************************************************************************/
using TMPro;
using UnityEngine;


public class SafeOpenScript : MonoBehaviour
{
    [SerializeField] private GameObject[] safeNumbers;
    [SerializeField] private GameObject safe;
    [SerializeField] private GameObject money;
    [SerializeField] private int[] codeNeeded;
    private bool locked;
    private int[] userCode;
    private int currentNum;
    void Start()
    {
        userCode = new int[safeNumbers.Length]; 
        locked = false; 
    }
    /// <summary>
    /// Updates the current safe code
    /// </summary>
    /// <param name="buttonKey"></param>
    public void UpdateButton(int buttonKey)
    {
        if(buttonKey < 0)
        {
            ResetCode();        
        }
        else if(buttonKey > 999)
        {
            ValidateCode();
        }
        else if (currentNum < safeNumbers.Length)
        {
            safeNumbers[currentNum].GetComponent<TMP_Text>().text = buttonKey.ToString();
            userCode[currentNum] = buttonKey;
            currentNum++;
        }
    }
    /// <summary>
    /// Checks if the player has the correct code
    /// </summary>
    public void ValidateCode()
    {
        if (!locked)
        {
            if (CodeValid())
            {
                GameObject.FindFirstObjectByType<AddToInventory>().AddNonWorldItem(money);
                GameObject.FindFirstObjectByType<TextPrompts>().ShowPrompt("" +
                    "You input a code and the safe opens. You find a few dollars inside");
                locked = true;
                safe.GetComponent<ObjectProperties>().usesAltSprites = true;
                safe.GetComponent<SpriteRenderer>().sprite = safe.GetComponent<ObjectProperties>().altSprite1;
            }
            else
            {
                GameObject.FindFirstObjectByType<TextPrompts>().ShowPrompt("" +
                    "You input a code and nothing happens");
            }
        }
        else
        {
            GameObject.FindFirstObjectByType<TextPrompts>().ShowPrompt("" +
                   "You look inside the safe, nothing more to see");
        }
        
    }
    /// <summary>
    /// Returns true if player has the correct code, else returns false
    /// </summary>
    /// <returns></returns>
    private bool CodeValid()
    {
        bool valid = true; 
        for(int i = 0; i < codeNeeded.Length; i++)
        {
            if (codeNeeded[i] != userCode[i])
            {
                valid = false;      
            }
        }
        return valid;       
    }
    /// <summary>
    /// Resets the current code the player inputs
    /// </summary>
    private void ResetCode()
    {
        foreach (GameObject g in safeNumbers)
        {
            g.GetComponentInChildren<TMP_Text>().text = "0";
        }
        for(int i = 0; i < safeNumbers.Length; i++)
        {
            userCode[i] = 0;
        }
        currentNum = 0;     
    }
    
}
