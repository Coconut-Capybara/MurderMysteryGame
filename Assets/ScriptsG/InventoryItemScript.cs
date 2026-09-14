/*****************************************************************************
// Script Name : InventoryItemScript
// Author : Gabriel Andrews
// Additional Author(s) :
// Creation Date:9/7/26
// Last Modified Date: 9/12/26
//
// Summary : Allows the player to interact with the items in their inventory
*****************************************************************************/
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class InventoryItemScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private int itemId;
    private bool grabbed;
    private PlayerInteract player;
    private Vector2 orgin;
    private bool isValid;
    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        if (isValid && player.cursorState != PlayerInteract.CursorState.HoldingItem)
        {
            player.cursorState = PlayerInteract.CursorState.InInventory;
            player.SetCurrentItem(this.gameObject); 
        }
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        if(isValid && player.cursorState != PlayerInteract.CursorState.HoldingItem)
        {
            player.cursorState = PlayerInteract.CursorState.None;
            player.SetCurrentItem(null);
        }      
    }
    void Start()
    {
        player = GameObject.FindFirstObjectByType<PlayerInteract>();            
    }
    public void ReturnToPos()
    {
        grabbed = false; 
        GetComponent<RectTransform>().anchoredPosition = orgin;
    }
    void Update()
    {
        if (grabbed)
        {
            Vector2 mousePosition = Mouse.current.position.ReadValue();
            GetComponent<RectTransform>().position = mousePosition;
        }
    }
    public int GetItemId()
    {
        return itemId;
    }
    public void SetIsGrabbed()
    {
        grabbed = true; 
    }
    public void SetOrgin(Vector2 input)
    {
        orgin = input;      
    }
    public void SetValid()
    {
        isValid = true; 
    }
}
