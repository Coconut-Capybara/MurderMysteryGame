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
    [SerializeField] private GameObject canvas;
    /// <summary>
    /// Puts item in inventory
    /// </summary>
    public void AddItem(GameObject item)
    {
        item.GetComponent<GrabbableObject>().SetIconPos(currentIconPos);
        item.GetComponent<GrabbableObject>().MoveIcon();
        currentIconPos += iconPivot;  
    }

    public void AddNonWorldItem(GameObject item)
    {
        item.GetComponent<RectTransform>().anchoredPosition = currentIconPos;
        GameObject itemIcon = Instantiate(item, currentIconPos, Quaternion.identity);
        itemIcon.GetComponent<InventoryItemScript>().SetOrgin(currentIconPos);
        itemIcon.transform.SetParent(canvas.transform, false);
        itemIcon.GetComponent<InventoryItemScript>().SetValid();
        currentIconPos += iconPivot;
    }
    public void ShiftLeft()
    {
        currentIconPos -= iconPivot;
        GameObject[] inventory = GameObject.FindGameObjectsWithTag("InInventory");
        foreach(GameObject item in inventory)
        {
            item.GetComponent<RectTransform>().anchoredPosition = (currentIconPos - iconPivot);
            item.GetComponent<InventoryItemScript>().SetOrgin(currentIconPos - iconPivot);   
        }
    }
}
