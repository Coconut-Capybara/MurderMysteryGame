/*****************************************************************************
// Script Name : ItemInspection
// Author : Gabriel Andrews
// Additional Author(s) :
// Creation Date:9/10/26
// Last Modified Date: 9/12/26
//
// Summary : Allows player tor read the description of certain items (early)
*****************************************************************************/
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class ItemInspections : MonoBehaviour
{
    [SerializeField] private GameObject itemDescPanel;
    [SerializeField] private TMP_Text itemDesc;
    [SerializeField] private float textSpeed;
    private PlayerInteract player;
    private InputAction inspect;
    private bool descLocked;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindFirstObjectByType<PlayerInteract>();
        inspect = InputSystem.actions.FindAction("Inspect");
        descLocked = false;
    }
    /// <summary>
    /// Shows Item Description
    /// </summary>
    private void Inspect()
    {
        Vector3 cursorPos = Mouse.current.position.ReadValue();
        Ray ray = Camera.main.ScreenPointToRay(cursorPos);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if(hit.collider.GetComponent<Descriptions>() != null)
            {
                ShowItemDescPanel(hit.collider.gameObject);
            }
        }
    }
    private void ShowItemDescPanel(GameObject item)
    {
        if (!descLocked)
        {
            itemDescPanel.SetActive(true);
            StartCoroutine(ShowText(item.GetComponent<Descriptions>().GetDescription()));
            descLocked = true;
        }
    }
    public void HideItemDescPanel()
    {
        descLocked = false; 
        itemDescPanel.SetActive(false);
        itemDesc.text = " ";
    }
    private IEnumerator ShowText(string text)
    {
        int i = 0;
        while (i < text.Length)
        {
            itemDesc.text += text[i];
            i++;
            yield return new WaitForSeconds(textSpeed);
        }     
    }
    // Update is called once per frame
    void Update()
    {
        if (inspect.WasPressedThisFrame())
        {
            Inspect();      
        }
    }
}
