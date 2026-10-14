/*****************************************************************************
// Script Name : AnimalPuzzleReset
// Author : Gabriel Andrews
// Additional Author(s) :
// Creation Date:10/05/26
// Last Modified Date: 10/14/26
//
// Summary : Resets the animal head puzzle
*****************************************************************************/
using UnityEngine;

public class AnimalPuzzleReset : NoItemInteractions
{
    public override void NoItemFunciton()
    {
        GameObject.FindFirstObjectByType<TaxidermyPuzzle>().ResetPuzzle();      
    }
}
