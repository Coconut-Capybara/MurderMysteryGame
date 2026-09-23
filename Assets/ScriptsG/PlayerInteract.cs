/*****************************************************************************
// Script Name : PlayerInteract
// Author : Gabriel Andrews
// Additional Author(s) :
// Creation Date:9/7/26
// Last Modified Date: 9/19/26
//
// Summary : Handles all player input, and changes the cursor state
*****************************************************************************/
using System.Runtime.CompilerServices;
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
    public bool inPrompt;
    /// <summary>
    /// The current state of the players cursor
    /// </summary>
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
        if (!inPrompt)
        {
            Vector3 cursorPos = Mouse.current.position.ReadValue();
            Ray ray = Camera.main.ScreenPointToRay(cursorPos);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.GetComponent<ObjectProperties>() != null)
                {
                    if (hit.collider.GetComponent<ObjectProperties>().isGrabbable ||
                        hit.collider.GetComponent<ObjectProperties>().isInteractable ||
                        hit.collider.GetComponent<ObjectProperties>().canUseOn)
                    {
                        if (cursorState is not (CursorState.InInventory or CursorState.HoldingItem)
                            && hit.collider.GetComponent<DoorScript>() == null)
                        {
                            cursorState = CursorState.OverObject;
                        }
                    }
                     if (hit.collider.GetComponent<DoorScript>() != null && cursorState is not (CursorState.InInventory
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
            else if (cursorState is not (CursorState.InInventory or CursorState.HoldingItem))
            {
                cursorState = CursorState.None;
                GameObject.FindFirstObjectByType<ItemInspections>().HideItemDescPanel();
            }


            if (interact.WasPressedThisFrame() && cursorState == CursorState.OverObject)
            {
                Interact(hit.collider.gameObject);
                
            }
            if (interact.WasPressedThisFrame() && cursorState == CursorState.InInventory)
            {
                currentItem.GetComponent<InventoryItemScript>().SetIsGrabbed();
                cursorState = CursorState.HoldingItem;
            }
            if (interact.WasReleasedThisFrame() && cursorState == CursorState.HoldingItem)
            {
                ItemUsageCheck(hit.collider.gameObject, currentItem);
                currentItem.GetComponent<InventoryItemScript>().ReturnToPos();
                currentItem = null;
                cursorState = CursorState.None;
            }
            if (interact.WasPressedThisFrame() && cursorState == CursorState.OverDoor)
            {
                DoorCheck();
            }
        }
    }
    /// <summary>
    /// Checks if the player clicked a door
    /// </summary>
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
    /// <summary>
    /// checks if the player used an item on another item
    /// </summary>
    /// <param name="_hoverItem"></param>
    /// <param name="_currentItem"></param>
    private void ItemUsageCheck(GameObject _hoverItem, GameObject _currentItem)
    {
        if(_hoverItem.GetComponent<ObjectProperties>() != null)
        {
            if (_hoverItem.GetComponent<ObjectProperties>().canUseOn)
            {
                hoverItem = _hoverItem;
                lockedItem = _currentItem;
                if (_currentItem.GetComponent<InventoryItemScript>().GetItemId()
                    == hoverItem.GetComponent<ItemNeeded>().GetItemNeeded().GetComponent<InventoryItemScript>().GetItemId())
                {
                    itemUseLock = true;
                    if (hoverItem.GetComponent<ObjectProperties>().timeUsage > 0)                        
                    {
                        TimeCheck();
                    }
                    
                    else
                    {
                        hoverItem.GetComponent<ItemNeeded>().ItemUsage(_currentItem);
                        if (_hoverItem.GetComponent<ObjectProperties>().givesPrompt)
                        {
                            GetComponent<TextPrompts>().ShowPrompt(_hoverItem.GetComponent<ObjectProperties>()
                                .prompt);
                        }
                    }
                }
            }
        }
        
    }
    /// <summary>
    /// Performs objects interaction, when interacted with
    /// </summary>
    private void Interact(GameObject item)
    {
        hoverItem = item;
        if (item.GetComponent<ObjectProperties>().isGrabbable)
        {
            if(item.GetComponent<ObjectProperties>().timeUsage > 0)
            {            
                TimeCheck();
            }
            
            else
            {
                item.GetComponent<GrabbableObject>().Collected();
                if (item.GetComponent<ObjectProperties>().givesPrompt)
                {
                    GetComponent<TextPrompts>().ShowPrompt(item.GetComponent<ObjectProperties>()
                        .prompt);
                }
            }
        }
        else if (item.GetComponent<ObjectProperties>().isInteractable)
        {
            if(item.GetComponent<ObjectProperties>().timeUsage > 0 && !hoverItem.GetComponent<ObjectProperties>().timeLock)
            {
                TimeCheck();    
            }
            else
            {
                item.GetComponent<NoItemInteractions>().NoItemFunciton();
                if (item.GetComponent<ObjectProperties>().givesPrompt)
                {
                    GetComponent<TextPrompts>().ShowPrompt(item.GetComponent<ObjectProperties>()
                        .prompt);
                }
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
    /// <summary>
    /// Turns on time panel
    /// </summary>
    private void TimeCheck()
    {
        GameObject fuckassRock = FindAnyObjectByType<RockScript>().gameObject;
        fuckassRock.GetComponent<ObjectProperties>().timeUsage = 5;
        timeController.PassTimePanel(hoverItem.GetComponent<ObjectProperties>().timeUsage,hoverItem);
        cursorState = CursorState.None;
        inPrompt = true;
    }
    /// <summary>
    /// Confirms the player wants to pass time
    /// </summary>
    public void TimeConfirm()
    {
        inPrompt = false;   
        timeController.timeUsePanel = false;
        timeController.TimeAway(hoverItem.GetComponent<ObjectProperties>().timeUsage);
        timeController.timePanel.SetActive(false);
        hoverItem.GetComponent<ObjectProperties>().timeUsage = 0;
        if (hoverItem.GetComponent<RockScript>() != null)
        {
            //hoverItem.GetComponent<RockScript>().timeLocked = true;
        }
        if (itemUseLock)
        {
            ItemUsageCheck(hoverItem, lockedItem);
        }
        else
        {
            hoverItem.GetComponent<ObjectProperties>().timeLock = true;
            Interact(hoverItem);            
        }
    }

    public void HidePrompt()
    {
       GameObject.FindFirstObjectByType<TextPrompts>().HidePrompt();
    }

}
