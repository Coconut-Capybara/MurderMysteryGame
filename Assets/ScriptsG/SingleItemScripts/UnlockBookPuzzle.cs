using UnityEngine;

public class UnlockBookPuzzle : ItemNeeded
{
    [SerializeField] private GameObject book1,book2;
    [SerializeField] private GameObject bookShelf;
    public override void ItemUsage(GameObject currentItem)
    {
        for (int i = 0; i < itemNeeded.Length; i++)
        {
            if (currentItem.GetComponent<InventoryItemScript>().GetItemId() ==
                itemNeeded[i].GetComponent<InventoryItemScript>().GetItemId())
            {
                book1.SetActive(true);
                book2.SetActive(true);
                GameObject.FindFirstObjectByType<StartBookPuzzle>().UnlockPuzzle();
                bookShelf.GetComponent<ObjectProperties>().timeLock = false;
                bookShelf.GetComponent<ObjectProperties>().timeUsage = 10;
                GetComponent<ObjectProperties>().canUseOn = false;
                Consume(currentItem);   
                ItemUseFunctionality(currentItem);  
            }
        }
    }
}
