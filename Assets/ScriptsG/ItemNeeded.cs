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

    public int timeUsedTesting;
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
                    timeController.PassTimePanel(timeUsedTesting);
                }
                else if(itemNeeded[i].GetComponent<InventoryItemScript>().GetItemId() == 2)
                {
                    print("Thing 2");
                }
            }
        }
    }
    public GameObject GetItemNeeded()
    {
        return itemNeeded[0];       
    }
}
