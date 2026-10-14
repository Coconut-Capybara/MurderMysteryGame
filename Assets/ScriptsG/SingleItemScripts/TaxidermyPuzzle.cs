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
    [SerializeField] private int timeAway;
    [SerializeField] private int[] headOrder;
    [SerializeField] private int[] orderNeeded;
    [SerializeField] private GameObject[] heads;
    [SerializeField] private Vector3 headPos;
    private Vector3 headPosOrgin;
    private int headCount;

    private void Start()
    {
        headPosOrgin = headPos;
    }
    public void UpdateValues(int headValue)
    {
        headOrder[headCount] = headValue;
        headCount++;
        headPos.x += pivot;       
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
        if (PuzzleCheck())
        {
            print("yes");
        }
        else
        {
            print("No");
        }
        GameObject.FindAnyObjectByType<TimeController>().TimeAway(timeAway);
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
    public Vector3 GetHeadPosition()
    {
        return headPos; 
    }
    public int GetHeadCount()
    {
        return headCount;       
    }
}

