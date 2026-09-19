using UnityEngine;

public class GivePlayerItem : NoItemInteractions
{
    [SerializeField] private GameObject itemPrefab;
    [SerializeField] private int amount;


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
