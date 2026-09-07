using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class InventoryItemScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public bool grabbed;
    private PlayerInteract player;
    public Vector2 orgin;
    public bool isValid;
    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        if (isValid && player.cursorState != PlayerInteract.CursorState.HoldingItem)
        {
            print("Grabbed");
            player.cursorState = PlayerInteract.CursorState.InInventory;
            player.currentItem = this.gameObject;
        }
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        if(isValid && player.cursorState != PlayerInteract.CursorState.HoldingItem)
        {
            player.cursorState = PlayerInteract.CursorState.None;
            player.currentItem = null;
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
}
