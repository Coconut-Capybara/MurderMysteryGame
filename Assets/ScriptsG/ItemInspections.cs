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
using UnityEngine;
using UnityEngine.InputSystem;

public class ItemInspections : MonoBehaviour
{
    [SerializeField] private GameObject itemDescPanel;
    [SerializeField] private TMP_Text itemDesc;
    [SerializeField] private float textSpeed;
    [SerializeField] private float delay;
    private bool descLocked;
    [SerializeField] private bool inDelay;
    private Coroutine itemDescCO;
    private Coroutine inspectCO;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        descLocked = false;
        inDelay = false;    
    }
    /// <summary>
    /// Shows Item Description
    /// </summary>
    private IEnumerator Inspect()
    {
        inDelay = true;
        yield return new WaitForSeconds(delay);     
        Vector3 cursorPos = Mouse.current.position.ReadValue();
        Ray ray = Camera.main.ScreenPointToRay(cursorPos);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if(hit.collider.GetComponent<ObjectProperties>() != null)
            {
                ShowItemDescPanel(hit.collider.gameObject);
            }
        }
    }
    /// <summary>
    /// shows description panel, starts text coroutine
    /// </summary>
    /// <param name="item"></param>
    private void ShowItemDescPanel(GameObject item)
    {
        if (!descLocked)
        {
            itemDescPanel.SetActive(true);
            itemDescCO = StartCoroutine(ShowText(item.GetComponent<ObjectProperties>().itemDesc));
            descLocked = true;
        }
    }
    /// <summary>
    /// hides description panel
    /// </summary>
    public void HideItemDescPanel()
    {
        descLocked = false;
        inDelay = false;
        itemDescPanel.SetActive(false);
        if(itemDescCO != null)
        {
            StopCoroutine(itemDescCO);
        }
        if (inspectCO != null)
        {
            StopCoroutine(inspectCO);
        }
        itemDesc.text = " ";   
    }
    /// <summary>
    /// Displys text, character by character
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
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
        Vector3 cursorPos = Mouse.current.position.ReadValue();
        Ray ray = Camera.main.ScreenPointToRay(cursorPos);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject.GetComponent<ObjectProperties>() != null
                && hit.collider.gameObject.GetComponent<ObjectProperties>().itemDesc != " ")
            {
                if (!GameObject.FindFirstObjectByType<PlayerInteract>().inPrompt && !inDelay)
                {
                    inspectCO = StartCoroutine(Inspect());
                }
            }
        }
        
    }
}
