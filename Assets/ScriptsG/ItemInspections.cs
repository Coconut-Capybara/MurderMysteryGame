/*****************************************************************************
// Script Name : ItemInspection
// Author : Gabriel Andrews
// Additional Author(s) :
// Creation Date:9/10/26
// Last Modified Date: 9/12/26
//
// Summary : Allows player tor read the description of certain items (early)
*****************************************************************************/
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class ItemInspections : MonoBehaviour
{
    [SerializeField] private GameObject itemDescPanel;
    [SerializeField] private TMP_Text itemDesc;
    private PlayerInteract player;
    private InputAction inspect;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindFirstObjectByType<PlayerInteract>();
        inspect = InputSystem.actions.FindAction("Inspect");
    }
    private void Inspect()
    {
        Vector3 cursorPos = Mouse.current.position.ReadValue();
        Ray ray = Camera.main.ScreenPointToRay(cursorPos);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
           ShowItemDescPanel(hit.collider.gameObject);
        }
    }
    private void ShowItemDescPanel(GameObject item)
    {
        itemDescPanel.SetActive(true);
        itemDesc.text = item.GetComponent<GrabbableObject>().GetDescription();
    }
    public void HideItemDescPanel()
    {
        itemDescPanel.SetActive(false);
        itemDesc.text = " ";
    }
    // Update is called once per frame
    void Update()
    {
        if (inspect.WasPressedThisFrame() && player.cursorState == PlayerInteract.CursorState.OverObject)
        {
            Inspect();      
        }
    }
}
