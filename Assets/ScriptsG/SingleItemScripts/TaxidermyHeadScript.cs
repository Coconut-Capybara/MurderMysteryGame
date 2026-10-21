/*****************************************************************************
// Script Name : TaxidermyHeadScript
// Author : Gabriel Andrews
// Additional Author(s) :
// Creation Date:10/05/26
// Last Modified Date: 10/14/26
//
// Summary : Handles the logic of the individual animal heads
*****************************************************************************/
using UnityEngine;

public class TaxidermyHeadScript : NoItemInteractions
{
    [SerializeField] private TaxidermyPuzzle puzzle;
    [SerializeField] private Vector3 orgin;
    [SerializeField] private int value;
    private bool locked;
    /// <summary>
    /// returns the animal head to its set position
    /// </summary>
    public override void NoItemFunciton()
    {
        if (puzzle.GetPuzzleActive())
        {
            if (puzzle.GetHeadCount() < 3 && !locked)
            {
                transform.position = puzzle.GetHeadPosition();
                puzzle.UpdateValues(value);
                locked = true;
            }
        }
        else
        {
            puzzle.StartPuzzle();   
        }
    }
    public Vector3 GetOrgin()
    {
        return orgin;       
    }
    public void SetLockState(bool _locked)
    {
        locked = _locked;       
    }
}
