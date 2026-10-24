/*****************************************************************************
// Script Name : TaxidermyPuzzle
// Author : Gabriel Andrews
// Additional Author(s) :
// Creation Date:10/05/26
// Last Modified Date: 10/14/26
//
// Summary : Handles the logic of the taxidermy puzzle
*****************************************************************************/
using UnityEngine;

public class TaxidermyPuzzle : NoItemInteractions
{
    [SerializeField] private float pivot;
    [SerializeField] private float pivotY;
    [SerializeField] private int[] headOrder;
    [SerializeField] private int[] orderNeeded;
    [SerializeField] private GameObject[] heads;
    [SerializeField] private Vector3 headPos;
    [SerializeField] private GameObject confirmButton, resetButton,backArrow;
    [SerializeField] private GameObject headKey;
    private Vector3 headPosOrgin;
    private int headCount;
    private bool puzzleActive;

    private void Start()
    {
        
    }
    public void UpdateValues(int headValue)
    {
        headOrder[headCount] = headValue;
        headCount++;
        headPos.x += pivot;
        if (headCount == 1)
        {
            headPos.y = pivotY;
        }
        else
        {
            headPos.y = 47;
        }
    }
    /// <summary>
    /// Resets the puzzle
    /// </summary>
    public void ResetPuzzle()
    {
        headCount = 0;
        headPos = headPosOrgin;
        for(int i = 0; i < headOrder.Length; i++) 
        {
            headOrder[i] = 0;
        }
        for(int i = 0; i < heads.Length; i++)
        {
            heads[i].transform.position = heads[i].GetComponent<TaxidermyHeadScript>().GetOrgin();
            heads[i].GetComponent<TaxidermyHeadScript>().SetLockState(false); 
            heads[i].GetComponent<ObjectProperties>().timeLock = false;     
        }
    }
    public override void NoItemFunciton()
    {
        PuzzleConfirmation();
    }
    /// <summary>
    /// Checks if the puzzle is correct, takes time regardless
    /// </summary>
    public void PuzzleConfirmation()
    {
        bool valid = true;
        for(int i = 0;i < heads.Length;i++)
        {
            if(headOrder[i] == 0)
            {
              valid = false;
                GameObject.FindFirstObjectByType<TextPrompts>().ShowPrompt("Place all the heads first");
                ResetPuzzle();
                break;
            }
        }
        if(valid)
        {
            if (PuzzleCheck())
            {
                GameObject.FindFirstObjectByType<TextPrompts>().ShowPrompt("You move the heads around, " +
                    "and get a key from the wolf head");
                GameObject.FindFirstObjectByType<AddToInventory>().AddNonWorldItem(headKey);
                UpdateHeads();
            }
            else
            {
                GameObject.FindFirstObjectByType<TextPrompts>().ShowPrompt("You move the heads around, and nothing happens");
            }
            GameObject.FindFirstObjectByType<UpdateLoungeHeads>().UpdateHeads(headOrder);
            EndPuzzle();
        }
    }  
    /// <summary>
    /// returns true if the player has the right order, else returns false
    /// </summary>
    /// <returns></returns>
    private bool PuzzleCheck()
    {
        bool valid = true;
        for (int i = 0; i < orderNeeded.Length; i++)
        {            
            if (orderNeeded[i] != headOrder[i])
            {
                valid = false;
            }            
        }
        return valid;
    }
    public void UpdateHeads()
    {
        for (int i = 0; i < heads.Length; i++)
        {
            heads[i].GetComponent<ObjectProperties>().timeUsage[0] = 0;
        }
    }
    public void StartPuzzle()
    {
        headPosOrgin = headPos;
        ResetPuzzle();
        puzzleActive = true;
        UpdateHeads();
        confirmButton.SetActive(true);
        resetButton.SetActive(true);   
        backArrow.SetActive(false);
    }
    private void EndPuzzle()
    {
        confirmButton.SetActive(false);
        resetButton.SetActive(false);
        backArrow.SetActive(true);
        puzzleActive = false;
        for (int i = 0; i < heads.Length; i++)
        {
            heads[i].GetComponent<ObjectProperties>().timeUsage[0] = 30;
        }
        headPos.x = 57;
    }
    public Vector3 GetHeadPosition()
    {
        return headPos; 
    }
    public int GetHeadCount()
    {
        return headCount;       
    }
    public bool GetPuzzleActive()
    {
        return puzzleActive;
    }
    public void SetPuzzleActive(bool active)
    {
        puzzleActive = active;  
    }
}

