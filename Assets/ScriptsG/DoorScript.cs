/*****************************************************************************
// Script Name : DoorScript
// Author : Gabriel Andrews
// Additional Author(s) :
// Creation Date:9/12/26
// Last Modified Date: 9/12/26
//
// Summary : Will handle player movement through doors
*****************************************************************************/
using Unity.VisualScripting;
using UnityEngine;

public class DoorScript : ItemNeeded
{
    [SerializeField] private bool isLocked;

    public void DoorLogic()
    {
        if (isLocked)
        {
            print("Door locked get key");
        }
        else
        {
            EnterDoor();    
        }
    }
    public void EnterDoor()
    {
        //camera movement here
        print("You enter the door");
    }
    public void SetLockState()
    {
        isLocked = false;
    }
    public bool GetLockState()
    {
        return isLocked;       
    }
    public override void ItemUsage(GameObject currentItem)
    {
        if(GetItemNeeded().GetComponent<InventoryItemScript>().GetItemId() ==
            currentItem.GetComponent<InventoryItemScript>().GetItemId())
        {
            SetLockState();
            GetComponent<Descriptions>().SetDescription("Unlocked");
        }
    }
}
