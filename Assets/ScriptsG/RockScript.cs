/*****************************************************************************
// Script Name : RockScript
// Author : Gabriel Andrews
// Additional Author(s) :
// Creation Date:9/19/26
// Last Modified Date: 9/19/26
//
// Summary : Stupid rock functionality
*****************************************************************************/
using UnityEngine;

public class RockScript : ItemNeeded
{
    [SerializeField] private GameObject key;
    public bool timeLocked;
    /// <summary>
    /// breaks the rock when the player used the screwdriver on it 
    /// </summary>
    /// <param name="currentItem"></param>
    public override void ItemUsage(GameObject currentItem)
    {
        if(currentItem.GetComponent<InventoryItemScript>().GetItemId()
            == GetItemNeeded().GetComponent<InventoryItemScript>().GetItemId())
        {
            GetComponent<ObjectProperties>().givesPrompt = true;
            GetComponent<ObjectProperties>().prompt = "You use the screwdriver to break the key " +
                "off the rock.";
            GameObject.FindFirstObjectByType<AddToInventory>().AddNonWorldItem(key);
            Consume(currentItem);
            gameObject.SetActive(false);                          
        }
    }
}
