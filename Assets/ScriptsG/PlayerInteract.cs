/*****************************************************************************
// Script Name : PlayerInteract
// Author : Gabriel Andrews
// Additional Author(s) :
// Creation Date:9/7/26
// Last Modified Date: 9/15/26
//
// Summary : Handles all player input, and changes the cursor state
*****************************************************************************/
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] private Camera playerCam;
    InputAction interact;
    [SerializeField] private GameObject currentItem;
    [SerializeField] private GameObject hoverItem;
    private bool itemUseLock;
    private GameObject lockedItem;
    private TimeController timeController;
    public enum CursorState
    {
        None,
        OverObject,
        InInventory,
        HoldingItem,
        OverDoor,
    }
    public CursorState cursorState;
    void Start()
    {
        timeController = GameObject.FindFirstObjectByType<TimeController>();    
        interact = InputSystem.actions.FindAction("Interact");
        itemUseLock = false;
    }

    /// <summary>
    /// Shoots out ray to update cursor state, and find interactable objects
    /// </summary>
    void Update()
    {
        Vector3 cursorPos = Mouse.current.position.ReadValue();
        Ray ray = Camera.main.ScreenPointToRay(cursorPos);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if(hit.collider.GetComponent<ObjectProperties>() != null)
            {
                if (hit.collider.GetComponent<ObjectProperties>().isGrabbable ||
                    hit.collider.GetComponent<ObjectProperties>().isInteractable || 
                    hit.collider.GetComponent<ObjectProperties>().canUseOn)
                {
                    if(cursorState is not (CursorState.InInventory or CursorState.HoldingItem))
                    {
                        cursorState = CursorState.OverObject;
                    }                    
                }
                else if (hit.collider.GetComponent<DoorScript>() != null && cursorState is not (CursorState.InInventory
                     or CursorState.HoldingItem))
                {
                    cursorState = CursorState.OverDoor;
                }
            }
            else if (hit.collider.GetComponent<ObjectProperties>() == null && cursorState is not (CursorState.InInventory
                     or CursorState.HoldingItem))
            {
                cursorState = CursorState.None;
                GameObject.FindFirstObjectByType<ItemInspections>().HideItemDescPanel();
            }
        }
        else if(cursorState is not (CursorState.InInventory or CursorState.HoldingItem)) 
        {
            cursorState = CursorState.None;
            GameObject.FindFirstObjectByType<ItemInspections>().HideItemDescPanel();
        }


        if (interact.WasPressedThisFrame() && cursorState == CursorState.OverObject)
        {
            Interact(hit.collider.gameObject);
            if (hit.collider.gameObject.GetComponent<TextPrompts>())
            {
                hit.collider.gameObject.GetComponent<TextPrompts>().ShowPrompt();
            }
        }
        if(interact.WasPressedThisFrame()&& cursorState == CursorState.InInventory)
        {
            currentItem.GetComponent<InventoryItemScript>().SetIsGrabbed();
            cursorState = CursorState.HoldingItem;
        }
        if(interact.WasReleasedThisFrame() && cursorState == CursorState.HoldingItem)
        {
            ItemUsageCheck(hit.collider.gameObject,currentItem);
            currentItem.GetComponent<InventoryItemScript>().ReturnToPos();
            currentItem = null;
            cursorState = CursorState.None;
            if (hit.collider.gameObject.GetComponent<TextPrompts>())
            {
                hit.collider.gameObject.GetComponent<TextPrompts>().ShowPrompt();
            }
        }
        if(interact.WasPressedThisFrame() && cursorState == CursorState.OverDoor)
        {
            DoorCheck();
        }
    }
    private void DoorCheck()
    {
        Vector3 cursorPos = Mouse.current.position.ReadValue();
        Ray ray = playerCam.ScreenPointToRay(cursorPos);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if(hit.collider.GetComponent<DoorScript>() != null)
            {
                hit.collider.GetComponent<DoorScript>().DoorLogic();
            }
        }
    }
    private void ItemUsageCheck(GameObject _hoverItem, GameObject _currentItem)
    {
        if(_hoverItem.GetComponent<ObjectProperties>() != null)
        {
            if (_hoverItem.GetComponent<ObjectProperties>().canUseOn)
            {
                hoverItem = _hoverItem;
                lockedItem = _currentItem;
                //matched
                if (_currentItem.GetComponent<InventoryItemScript>().GetItemId()
                    == hoverItem.GetComponent<ItemNeeded>().GetItemNeeded().GetComponent<InventoryItemScript>().GetItemId())
                {
                    itemUseLock = true;
                    if (hoverItem.GetComponent<ObjectProperties>().timeUsage > 0)
                        
                    {
                        //take time
                        TimeCheck();
                    }
                    
                    else
                    {
                        //perform
                        hoverItem.GetComponent<ItemNeeded>().ItemUsage(_currentItem);
                    }
                }
            }
        }
    }
    /// <summary>
    /// Puts objects in inventory if able 
    /// </summary>
    private void Interact(GameObject item)
    {
        hoverItem = item;
        //grabbable
        if (item.GetComponent<ObjectProperties>().isGrabbable)
        {
            if(item.GetComponent<ObjectProperties>().timeUsage > 0)
            {
               //take time               
                TimeCheck();
            }
            
            else
            {
               //give item
                item.GetComponent<GrabbableObject>().Collected();
            }
        }
        //interactable
        else if (item.GetComponent<ObjectProperties>().isInteractable)
        {
            if(item.GetComponent<ObjectProperties>().timeUsage > 0
                && hoverItem.GetComponent<RockScript>() == null)//temp remove later
            {
                //take time
                TimeCheck();    
            }
            //temp remove later
            else if (hoverItem.GetComponent<RockScript>() != null)
            {
                if (!hoverItem.GetComponent<RockScript>().timeLocked)
                {
                    TimeCheck();
                }
            }
            else
            {
                //perform
                item.GetComponent<NoItemInteractions>().NoItemFunciton();   
            }
        }            
    }
    public GameObject GetCurrentItem()
    {
        return currentItem;
    }
    public void SetCurrentItem(GameObject item)
    {
        currentItem = item; 
    }
    private void TimeCheck()
    {
        //turns on panel
        timeController.PassTimePanel(hoverItem.GetComponent<ObjectProperties>().timeUsage);
    }
    public void TimeConfirm()
    {
        timeController.timeUsePanel = false;
        timeController.TimeAway(hoverItem.GetComponent<ObjectProperties>().timeUsage);
        timeController.timePanel.SetActive(false);
        hoverItem.GetComponent<ObjectProperties>().timeUsage = 0;
        if (itemUseLock)
        {
            ItemUsageCheck(hoverItem, lockedItem);
        }
        else
        {
            Interact(hoverItem);
            //temp remove later
            if (hoverItem.GetComponent<RockScript>()  != null)
            {
                hoverItem.GetComponent<RockScript>().timeLocked = true;
                hoverItem.GetComponent<ObjectProperties>().timeUsage += 5;
            }
        }
    }

}
