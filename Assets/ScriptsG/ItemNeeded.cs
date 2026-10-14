/*****************************************************************************
// Script Name : ItemNeeded
// Author : Gabriel Andrews
// Additional Author(s) :
// Creation Date:9/10/26
// Last Modified Date: 9/12/26
//
// Summary : Put this on objects intended to have inventory items used on
*****************************************************************************/
using UnityEngine;

public class ItemNeeded : MonoBehaviour
{
    [Tooltip("The list of items that will work when used on this object")]
    [SerializeField] private GameObject[] itemNeeded;
    [Tooltip("The Script TimeController. Should only be one in the scene")]
    [SerializeField] private TimeController timeController;

    [Tooltip("Gives player an item")]
    [SerializeField] private bool giveItem;

    [Tooltip("Item given to player")]
    [SerializeField] private GameObject ItemGiven;

    [Tooltip("Disables the object the item is being used on")]
    [SerializeField] private bool disableObject;

    /// <summary>
    /// Base function for item interaction
    /// </summary>
    /// <param name="currentItem"></param>
    public virtual void ItemUsage(GameObject currentItem)
    {
        for(int i = 0; i < itemNeeded.Length; i++)
        {
            if(currentItem.GetComponent<InventoryItemScript>().GetItemId() ==
                itemNeeded[i].GetComponent<InventoryItemScript>().GetItemId())
            {
                ItemUseFunctionality(currentItem);
            }
        }
    }
    private void ItemUseFunctionality(GameObject currentItem)
    {
        if(giveItem)
        {
            GameObject.FindFirstObjectByType<AddToInventory>().AddNonWorldItem(currentItem);
        }
        if (currentItem.GetComponent<ObjectProperties>().consumable)
        {
            Consume(currentItem);
        }
        if (disableObject)
        {
            gameObject.SetActive(false);        
        }
    }
    /// <summary>
    /// Deactivates objects marked "consumable" after they're used
    /// </summary>
    /// <param name="currentItem"></param>
    public void Consume(GameObject currentItem)
    {
        if(currentItem.GetComponent<ObjectProperties>() != null &&
            currentItem.GetComponent<ObjectProperties>().consumable)
        {           
            currentItem.SetActive(false);
            GameObject.FindAnyObjectByType<AddToInventory>().ShiftLeft();
        }
    }
    public GameObject GetItemNeeded()
    {
        return itemNeeded[0];       
    }
}
