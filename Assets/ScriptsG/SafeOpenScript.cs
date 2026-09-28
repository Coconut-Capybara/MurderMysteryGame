using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SafeOpenScript : MonoBehaviour
{
    [SerializeField] private GameObject[] safeNumbers;
    [SerializeField] private int[] codeNeeded;
    private int[] userCode;
    private int currentNum;
    void Start()
    {
        userCode = new int[safeNumbers.Length];    
    }
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
            switch (buttonKey)
            {
                case 0: safeNumbers[currentNum].GetComponentInChildren<TMP_Text>().text = buttonKey.ToString(); break;
                case 1: safeNumbers[currentNum].GetComponentInChildren<TMP_Text>().text = buttonKey.ToString(); break;
                case 2: safeNumbers[currentNum].GetComponentInChildren<TMP_Text>().text = buttonKey.ToString(); break;
                case 3: safeNumbers[currentNum].GetComponentInChildren<TMP_Text>().text = buttonKey.ToString(); break;
            }
            userCode[currentNum] = buttonKey;
            currentNum++;
        }
    }
    public void ValidateCode()
    {
        if (CodeValid())
        {
            print("Correct");
        }
        else
        {
            print("Wrong");
        }
    }
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
    private void ResetCode()
    {
        foreach (GameObject g in safeNumbers)
        {
            g.GetComponentInChildren<TMP_Text>().text = " ";
        }
        for(int i = 0; i < safeNumbers.Length; i++)
        {
            userCode[i] = 0;
        }
        currentNum = 0;     
    }
    
}
