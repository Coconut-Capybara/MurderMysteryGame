/*****************************************************************************
// Script Name : GrabbableObject
// Author : Gabriel Andrews
// Additional Author(s) :
// Creation Date:9/7/26
// Last Modified Date: 9/12/26
//
// Summary : Put this script on an item you want to be added to the players
inventory
*****************************************************************************/
using UnityEngine;
public class GrabbableObject : MonoBehaviour
{
    [SerializeField] private GameObject itemIconGO;
    [SerializeField] private GameObject canvas;
    [SerializeField] private string description;
    private Vector2 iconPos;
    /// <summary>
    /// handles inventory logic
    /// </summary>
    public void Collected()
    {
        gameObject.SetActive(false);    
        GameObject.FindFirstObjectByType<AddToInventory>().AddItem(this.gameObject);   
    }
    /// <summary>
    /// Converts the in game object to an inventory item
    /// </summary>
    /// <returns></returns>
    public void MoveIcon()
    {       
        GameObject itemIcon = Instantiate(itemIconGO,iconPos,Quaternion.identity);
        itemIcon.GetComponent<InventoryItemScript>().SetOrgin(iconPos);
        itemIcon.transform.SetParent(canvas.transform, false);
        itemIcon.GetComponent<InventoryItemScript>().SetValid();
    }
    public string GetDescription()
    {
        return description;     
    }
    public void SetIconPos(Vector2 input)
    {
        iconPos = input;
    }
}
