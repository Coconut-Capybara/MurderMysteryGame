/*****************************************************************************
// Script Name : GivePlayerItem
// Author : Gabriel Andrews
// Additional Author(s) : Bryson Welch
// Creation Date: 9/19/26
// Last Modified Date: 10/5/26
//
// Summary : Creates item, and puts it in players inventory
*****************************************************************************/
using UnityEngine;

public class GivePlayerItem : NoItemInteractions
{
    [Tooltip("The items to be given to the player when interacted with")]
    [SerializeField] private GameObject[] itemPrefabs;
    [Tooltip("The amount of times items can be given can be done for this object")]
    [SerializeField] private int amount;
    [Tooltip("Reference to the rock to fix an edge case bug")]
    [SerializeField] private GameObject outsideRock;

    /// <summary>
    /// Function for objects meant to be interacted with, with no item
    /// </summary>
    public override void NoItemFunciton()
    {
        for (int i = 0; i < itemPrefabs.Length; i++)
        {
            GiveItem(itemPrefabs[i]);
            if (itemPrefabs[i].gameObject.name == "Screwdriver")
            {
                outsideRock.GetComponent<ObjectProperties>().timeLock = false;
            }
            if(gameObject.name == "2D_DumpsterInspect")
            {
                GetComponent<ObjectProperties>().prompt = "I don't think theres anything left in here";
            }
        }

    }
    public void GiveItem(GameObject item)
    {
        if(amount > 0)
        {
            for(int i = 0; i < itemPrefabs.Length; i++)
            {
                GameObject.FindFirstObjectByType<AddToInventory>().AddNonWorldItem(itemPrefabs[i]);
            }

            amount--;
        }
    }
}
