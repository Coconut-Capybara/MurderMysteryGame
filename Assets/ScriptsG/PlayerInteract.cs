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
    private GameObject currentItem;
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
        interact = InputSystem.actions.FindAction("Interact");
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
        }
        if(interact.WasPressedThisFrame()&& cursorState == CursorState.InInventory)
        {
            currentItem.GetComponent<InventoryItemScript>().SetIsGrabbed();
            cursorState = CursorState.HoldingItem;
        }
        if(interact.WasReleasedThisFrame() && cursorState == CursorState.HoldingItem)
        {
            ItemUsageCheck();
            currentItem.GetComponent<InventoryItemScript>().ReturnToPos();
            currentItem = null;
            cursorState = CursorState.None;
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
    private void ItemUsageCheck()
    {
        
        Vector3 cursorPos = Mouse.current.position.ReadValue();
        Ray ray = playerCam.ScreenPointToRay(cursorPos);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.GetComponent<ObjectProperties>() != null &&
                hit.collider.GetComponent<ItemNeeded>() != null)
            {
                hit.collider.GetComponent<ItemNeeded>().ItemUsage(currentItem);
            }
        }                  
    }
    /// <summary>
    /// Puts objects in inventory if able 
    /// </summary>
    private void Interact(GameObject item)
    {        
        Vector3 cursorPos = Mouse.current.position.ReadValue();
        Ray ray = playerCam.ScreenPointToRay(cursorPos);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.GetComponent<ObjectProperties>().isGrabbable)
            {
                item.GetComponent<GrabbableObject>().Collected();
            }
            else if (hit.collider.GetComponent<GivePlayerItem>() != null && 
                hit.collider.GetComponent<ObjectProperties>().isInteractable)
            {
                hit.collider.GetComponent<GivePlayerItem>().GiveItem(); 
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
}
