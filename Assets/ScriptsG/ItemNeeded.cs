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
    [SerializeField] private GameObject[] itemNeeded;
    [SerializeField] private TimeController timeController;
    /// <summary>
    /// Base function for item interaction
    /// </summary>
    /// <param name="currentItem"></param>
    public virtual void ItemUsage(GameObject currentItem)
    {
        for(int i = 0; i < itemNeeded.Length; i++)
        {
            if(itemNeeded[i].GetComponent<InventoryItemScript>().GetItemId() ==
                currentItem.GetComponent<InventoryItemScript>().GetItemId())
            {
                if(itemNeeded[i].GetComponent<InventoryItemScript>().GetItemId() == 1)
                {
                    print("Thing 1");
                }
                else if(itemNeeded[i].GetComponent<InventoryItemScript>().GetItemId() == 2)
                {
                    print("Thing 2");
                }
            }
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
