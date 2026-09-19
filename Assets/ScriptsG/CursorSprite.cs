/*****************************************************************************
// Script Name : CursorSprite
// Author : Gabriel Andrews
// Additional Author(s) :
// Creation Date:9/12/26
// Last Modified Date: 9/14/26
//
// Summary : Changes the cursor to the appropriet sprite
*****************************************************************************/
using UnityEngine;

public class CursorSprite : MonoBehaviour
{
    [SerializeField] private Texture2D baseCursorSprite;
    [SerializeField] private Texture2D inventorySprite;
    [SerializeField] private Texture2D overObjectSprite;
    [SerializeField] private Texture2D holdingObjectSprite;
    [SerializeField] private Texture2D overDoorSprite;
    [SerializeField] private Texture2D inspectionSprite;
    [SerializeField] private Vector2 pivot;
    private PlayerInteract player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindFirstObjectByType<PlayerInteract>();        
    }

    // Update is called once per frame
    void Update()
    {
        switch(player.cursorState)
        {
            case PlayerInteract.CursorState.None:
                Cursor.SetCursor(baseCursorSprite,new Vector2(18.2f,0), CursorMode.Auto); break;
            case PlayerInteract.CursorState.InInventory:
                Cursor.SetCursor(inventorySprite, new Vector2(24, 6), CursorMode.Auto); break;
            case PlayerInteract.CursorState.HoldingItem:
                Cursor.SetCursor(holdingObjectSprite, new Vector2(24, 6), CursorMode.Auto); break;
            case PlayerInteract.CursorState.OverObject:
                Cursor.SetCursor(overObjectSprite, new Vector2 (23,11), CursorMode.Auto); break;
            case PlayerInteract.CursorState.OverDoor:
                Cursor.SetCursor(overDoorSprite, Vector2.zero, CursorMode.Auto); break;
            default:
                Cursor.SetCursor(baseCursorSprite, Vector2.zero, CursorMode.Auto); break;
        }
    }
}
