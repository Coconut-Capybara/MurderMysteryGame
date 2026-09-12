/*****************************************************************************
// Script Name : PlayerInteract
// Author : Gabriel Andrews
// Additional Author(s) :
// Creation Date:9/7/26
// Last Modified Date: 9/12/26
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
        HoldingItem
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
        Ray ray = playerCam.ScreenPointToRay(cursorPos);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject.GetComponent<GrabbableObject>() != null && cursorState is not (CursorState.InInventory
                 or CursorState.HoldingItem))
            {
                cursorState = CursorState.OverObject;
            }
            else if (hit.collider.gameObject.GetComponent<GrabbableObject>() == null && cursorState is not (CursorState.InInventory
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
        }
    }
    private void ItemUsageCheck()
    {
        
        Vector3 cursorPos = Mouse.current.position.ReadValue();
        Ray ray = playerCam.ScreenPointToRay(cursorPos);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.GetComponent<ItemNeeded>() != null)
            {                
                hit.collider.GetComponent<ItemNeeded>().ItemUsage(currentItem);
            }
        }          
        currentItem.GetComponent<InventoryItemScript>().ReturnToPos();
        currentItem = null;
        cursorState = CursorState.None;        
    }
    /// <summary>
    /// Puts objects in inventory if able 
    /// </summary>
    private void Interact(GameObject item)
    {
        item.GetComponent<GrabbableObject>().Collected();
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
