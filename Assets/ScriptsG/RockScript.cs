using UnityEngine;

public class RockScript : ItemNeeded
{
    public override void ItemUsage(GameObject currentItem)
    {
        if(currentItem.GetComponent<InventoryItemScript>().GetItemId()
            == GetItemNeeded().GetComponent<InventoryItemScript>().GetItemId())
        {
            GetComponent<GivePlayerItem>().GiveItem();
        }
    }
}
