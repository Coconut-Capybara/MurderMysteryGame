/*****************************************************************************
// Script Name : ItemNeeded
// Author : Gabriel Andrews
// Additional Author(s) :
// Creation Date:9/10/26
// Last Modified Date: 9/12/26
//
// Summary : Put this on objects intended to have inventory items used on
*****************************************************************************/
using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class ItemNeeded : MonoBehaviour
{
    [Tooltip("The list of items that will work when used on this object")]
    public GameObject[] itemNeeded;
    [Tooltip("The Script TimeController. Should only be one in the scene")]
    [SerializeField] private TimeController timeController;

    [Tooltip("Gives player an item")]
    [SerializeField] private bool giveItem;

    [Tooltip("Item given to player")]
    [SerializeField] private GameObject[] itemGiven;

    [Tooltip("Disables the object the item is being used on")]
    [SerializeField] private bool disableObject;

    [SerializeField] private bool changeSprite;

    [SerializeField] private bool enableCam;
    [SerializeField] private MovePointData camToEnable;

    [SerializeField] private bool enableItem;
    [SerializeField] private GameObject itemEnabled;

    [SerializeField] private bool givesHardEvidence;
    [SerializeField] private GameObject hardEvidenceGO;

    [SerializeField] private bool changesItemDesc;
    /// <summary>
    /// Base function for item interaction
    /// </summary>
    /// <param name="currentItem"></param>
    public virtual void ItemUsage(GameObject currentItem)
    {
        for (int i = 0; i < itemNeeded.Length; i++)
        {
            if (currentItem.GetComponent<InventoryItemScript>().GetItemId() ==
                itemNeeded[i].GetComponent<InventoryItemScript>().GetItemId())
            {
                ItemUseFunctionality(currentItem);
            }
        }
    }
    public void ItemUseFunctionality(GameObject currentItem)
    {
        if (currentItem.GetComponent<ObjectProperties>().consumable)
        {
            Consume(currentItem);
        }
        if (giveItem)
        {
            StartCoroutine(BufferAdd());
        }
        if (changeSprite)
        {
            GetComponent<ObjectProperties>().usesAltSprites = true;
            GetComponent<SpriteRenderer>().sprite = GetComponent<ObjectProperties>().altSprite1;
        }
        if (disableObject)
        {
            gameObject.SetActive(false);
        }
        if (enableCam)
        {
            GetComponent<MovePointData>().enabled = true;        
        }
        if (enableItem)
        {
            itemEnabled.SetActive(true);  
        }
        if (givesHardEvidence)
        {
            GameObject.FindFirstObjectByType<EndGameHandler>().CollectHardEvidence();
            hardEvidenceGO.SetActive(false);
        }
        changesItemDesc = GetComponent<ObjectProperties>().itemChangesDesc;
        if (changesItemDesc)
        {
            GetComponent<ObjectProperties>().usingItemChangeDesc = true;
        }
    }
    IEnumerator BufferAdd()
    {
        yield return new WaitForSeconds(0.1f);
        for (int i = 0; i < itemGiven.Length; i++)
        {
            GameObject.FindFirstObjectByType<AddToInventory>().AddNonWorldItem(itemGiven[i]);
            yield return new WaitForSeconds(0.1f);
        }

    }
    /// <summary>
    /// Deactivates objects marked "consumable" after they're used
    /// </summary>
    /// <param name="currentItem"></param>
    public void Consume(GameObject currentItem)
    {
        if (currentItem.GetComponent<ObjectProperties>() != null &&
            currentItem.GetComponent<ObjectProperties>().consumable)
        {
            currentItem.SetActive(false);
            currentItem.gameObject.tag = "Untagged";
            GameObject.FindAnyObjectByType<AddToInventory>().ShiftLeft();
            GameObject.FindFirstObjectByType<PlayerInteract>().UnlockItemUse();
        }
    }
    public GameObject GetItemNeeded()
    {
        return itemNeeded[0];
    }
    public bool ItemValid(GameObject item)
    {
        bool valid = false;
        for(int i = 0;i < itemNeeded.Length;i++)
        {
            if(itemNeeded[i].GetComponent<InventoryItemScript>().GetItemId()
                == item.GetComponent<InventoryItemScript>().GetItemId())
            {
                valid = true;   
            }
        }
        return valid;
    }
}
