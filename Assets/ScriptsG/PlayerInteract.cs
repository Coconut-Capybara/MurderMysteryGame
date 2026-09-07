using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] private Camera playerCam;
    InputAction interact;
    public GameObject currentItem;
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
            }     
        }
        else if(cursorState is not (CursorState.InInventory or CursorState.HoldingItem)) 
        {
            cursorState = CursorState.None;
        }


        if (interact.WasPressedThisFrame() && cursorState == CursorState.OverObject)
        {
            Interact(hit.collider.gameObject);
        }
        if(interact.WasPressedThisFrame()&& cursorState == CursorState.InInventory)
        {
            currentItem.GetComponent<InventoryItemScript>().grabbed = true;
            cursorState = CursorState.HoldingItem;
        }
        if(interact.WasReleasedThisFrame() && cursorState == CursorState.HoldingItem)
        {
            currentItem.GetComponent<InventoryItemScript>().ReturnToPos();  
            currentItem = null;
            cursorState = CursorState.None;
        }
    }
    /// <summary>
    /// Puts objects in inventory if able 
    /// </summary>
    private void Interact(GameObject item)
    {
        item.GetComponent<GrabbableObject>().Collected();
    }

 
}
