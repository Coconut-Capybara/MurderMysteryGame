using UnityEngine;

public class RockScript : ItemNeeded
{
    [SerializeField] private GameObject key;
    public bool timeLocked;
    public override void ItemUsage(GameObject currentItem)
    {
        if(currentItem.GetComponent<InventoryItemScript>().GetItemId()
            == GetItemNeeded().GetComponent<InventoryItemScript>().GetItemId())
        {
            GetComponent<ObjectProperties>().givesPrompt = true;
            GetComponent<ObjectProperties>().prompt = "You get the key from the rock";
            GameObject.FindFirstObjectByType<AddToInventory>().AddNonWorldItem(key);
            Consume(currentItem);
            gameObject.SetActive(false);                          
        }
    }
}
