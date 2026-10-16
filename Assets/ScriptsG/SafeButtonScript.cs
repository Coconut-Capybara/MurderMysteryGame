/*****************************************************************************
// Script Name : SafeButtonScript
// Author : Gabriel Andrews
// Additional Author(s) :
// Creation Date:9/26/26
// Last Modified Date: 10/14/26
//
// Summary : Handles the number buttons on the safe
*****************************************************************************/
using UnityEngine;

public class SafeButtonScript : NoItemInteractions
{
    [SerializeField] private int num;
    [SerializeField] private SafeOpenScript safe;

    public override void NoItemFunciton()
    {
        safe.UpdateButton(num); 
    }
   
}
