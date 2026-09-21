/*****************************************************************************
// Script Name : DoorScript
// Author : Gabriel Andrews
// Additional Author(s) :
// Creation Date:9/12/26
// Last Modified Date: 9/19/26
//
// Summary : Will handle player movement through doors
*****************************************************************************/
using Unity.VisualScripting;
using UnityEngine;

public class DoorScript : ItemNeeded
{
    [SerializeField] private bool isLocked;

    [SerializeField] private VertSliceEndScreen vertSliceEndScreen;
    private GameObject vertSlicePanel;
    /// <summary>
    /// Checks whether the door is locked, enters if so
    /// </summary>
    public void DoorLogic()
    {
        if (isLocked)
        {
            GameObject.FindFirstObjectByType<TextPrompts>().ShowPrompt("Door is locked, find key");
        }
        else
        {
            EnterDoor();    
        }
    }
    /// <summary>
    /// Moves camera to new position through door
    /// </summary>
    public void EnterDoor()
    {
        //camera movement here
        vertSliceEndScreen = FindAnyObjectByType<VertSliceEndScreen>();
        vertSlicePanel = vertSliceEndScreen.vertSlicePanel;
        vertSlicePanel.SetActive(true);
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
    /// <summary>
    /// checks if player is holding the correct key, unlocks door if so 
    /// </summary>
    /// <param name="currentItem"></param>
    public override void ItemUsage(GameObject currentItem)
    {
        if(GetItemNeeded().GetComponent<InventoryItemScript>().GetItemId() ==
            currentItem.GetComponent<InventoryItemScript>().GetItemId())
        {
            SetLockState();
            GetComponent<ObjectProperties>().itemDesc = "Unlocked door";
            Consume(currentItem);       
        }
    }
}
