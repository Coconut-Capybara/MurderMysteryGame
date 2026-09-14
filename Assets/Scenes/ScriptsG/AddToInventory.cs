/*****************************************************************************
// Script Name : AddToInventory
// Author : Gabriel Andrews
// Additional Author(s) :
// Creation Date:9/7/26
// Last Modified Date: 9/12/26
//
// Summary : Handles adding items to the players inventory
*****************************************************************************/

using UnityEngine;

public class AddToInventory : MonoBehaviour
{
    [SerializeField] private Vector2 currentIconPos;
    [SerializeField] private Vector2 iconPivot;
    /// <summary>
    /// Puts item in inventory
    /// </summary>
    public void AddItem(GameObject item)
    {
        item.GetComponent<GrabbableObject>().SetIconPos(currentIconPos);
        item.GetComponent<GrabbableObject>().MoveIcon();
        currentIconPos += iconPivot;  
    }
}
