/*****************************************************************************
// Script Name : RockScript
// Author : Gabriel Andrews
// Additional Author(s) :
// Creation Date:9/19/26
// Last Modified Date: 9/30/26
//
// Summary : Stupid rock functionality
*****************************************************************************/
using UnityEngine;

public class RockScript : ItemNeeded
{
    [SerializeField] private GameObject key;
    [SerializeField] private ObjectProperties objectProperties;
    [SerializeField] int itemUseTime;
    /// <summary>
    /// breaks the rock when the player used the screwdriver on it 
    /// </summary>
    /// <param name="currentItem"></param>
    public override void ItemUsage(GameObject currentItem)
    {
        if(currentItem.GetComponent<InventoryItemScript>().GetItemId() == 1)
        {
            GetComponent<ObjectProperties>().prompt = "You use the screwdriver to break the key " +
                "off the rock. You got the employee key, but the screwdriver broke.";
            GetComponent<ObjectProperties>().itemDesc = "Nothing else on the rock as far as I can tell.";
            ItemUseFunctionality(currentItem);
            //GameObject.FindFirstObjectByType<TextPrompts>().ShowPrompt(GetComponent<ObjectProperties>().prompt);
        }
        
    }
}
