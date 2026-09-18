using UnityEngine;

public class GivePlayerItem : MonoBehaviour
{
    [SerializeField] private GameObject itemPrefab;
    [SerializeField] private int amount;
    public void GiveItem()
    {
        if(amount > 0)
        {
            GameObject.FindFirstObjectByType<AddToInventory>().AddNonWorldItem(itemPrefab);
            amount--;
        }
    }
}
