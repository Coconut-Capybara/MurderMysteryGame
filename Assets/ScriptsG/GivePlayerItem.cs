/*****************************************************************************
// Script Name : GivePlayerItem
// Author : Gabriel Andrews
// Additional Author(s) :
// Creation Date:9/19/26
// Last Modified Date: 9/19/26
//
// Summary : Creates item, and puts it in players inventory
*****************************************************************************/
using UnityEngine;

public class GivePlayerItem : NoItemInteractions
{
    [SerializeField] private GameObject itemPrefab;
    [SerializeField] private int amount;

    /// <summary>
    /// Function for objects meant to be interacted with, with no item
    /// </summary>
    public override void NoItemFunciton()
    {
        GiveItem(itemPrefab);
    }
    public void GiveItem(GameObject item)
    {
        if(amount > 0)
        {
            GameObject.FindFirstObjectByType<AddToInventory>().AddNonWorldItem(itemPrefab);
            amount--;
        }
    }
}
