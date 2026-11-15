/*****************************************************************************
// Script Name : AddToInventory
// Author : Gabriel Andrews
// Additional Author(s) :
// Creation Date:9/7/26
// Last Modified Date: 9/19/26
//
// Summary : Handles adding items to the players inventory
*****************************************************************************/

using UnityEngine;

public class AddToInventory : MonoBehaviour
{
    [Tooltip("The inventory items current position in the inventory")]
    [SerializeField] private Vector2 currentIconPos;

    [Tooltip("The amount of space in between each item in the inventory")]
    [SerializeField] private Vector2 iconPivot;
    [SerializeField] private float pivotY;
    [SerializeField] private int wrapNum;

    [Tooltip("The inventory canvas")]
    [SerializeField] private GameObject canvas;

    private Vector2 slotOne;
    public int count;
    private void Start()
    {
        slotOne = currentIconPos;
    }
    /// <summary>
    /// Puts item in inventory
    /// </summary>
    public void AddItem(GameObject item)
    {
        item.GetComponent<GrabbableObject>().SetIconPos(currentIconPos);
        item.GetComponent<GrabbableObject>().MoveIcon();
        currentIconPos += iconPivot;  
        count++;
        if(count == wrapNum)
        {
            currentIconPos.y -= pivotY;
            currentIconPos.x = slotOne.x;
            count = 0;
        }
    }
    /// <summary>
    /// Instntiates inventory item, and adds it
    /// </summary>
    /// <param name="item"></param>
    public void AddNonWorldItem(GameObject item)
    {
        item.GetComponent<RectTransform>().anchoredPosition = currentIconPos;
        GameObject itemIcon = Instantiate(item, currentIconPos, Quaternion.identity);
        itemIcon.GetComponent<InventoryItemScript>().SetOrgin(currentIconPos);
        currentIconPos += iconPivot;
        itemIcon.transform.SetParent(canvas.transform, false);
        itemIcon.GetComponent<InventoryItemScript>().SetValid();
        count++;
        if (count == wrapNum)
        {
            currentIconPos.y -= pivotY;
            currentIconPos.x = slotOne.x;
            count = 0;
        }
    }
    /// <summary>
    /// Shifts the inventory items 1 slot left after
    /// a consumable is used
    /// </summary>
    public void ShiftLeft()
    {
        currentIconPos = slotOne;
        count = 0;
        GameObject[] inventory = GameObject.FindGameObjectsWithTag("InInventory");
        foreach(GameObject item in inventory)
        {         
            item.GetComponent<RectTransform>().anchoredPosition = currentIconPos;
            item.GetComponent<InventoryItemScript>().SetOrgin(currentIconPos);   
            currentIconPos += iconPivot;
            count++;
            if (count == wrapNum)
            {
                currentIconPos.y -= pivotY;
                currentIconPos.x = slotOne.x;
                count = 0;
            }
        }
    }
    public void HideInventory()
    {
        canvas.SetActive(false);    
    }
    public void ShowInventory()
    {
        canvas.SetActive(true); 
    }
}
