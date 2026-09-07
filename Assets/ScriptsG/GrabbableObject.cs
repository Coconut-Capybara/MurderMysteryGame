using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class GrabbableObject : MonoBehaviour
{
    public Image itemIcon;
    public Vector2 iconPos;
    /// <summary>
    /// handles inventory logic
    /// </summary>
    public void Collected()
    {
        gameObject.SetActive(false);    
        GameObject.FindFirstObjectByType<AddToInventory>().AddItem(this.gameObject);
        itemIcon.GetComponent<InventoryItemScript>().orgin = iconPos;   
    }
    /// <summary>
    /// Converts the in game object to an inventory item
    /// </summary>
    /// <returns></returns>
    public IEnumerator MoveIcon()
    {
        itemIcon.gameObject.SetActive(true);    
        Vector2 iconPosition = GetComponent<GrabbableObject>().iconPos;
        while (itemIcon.rectTransform.anchoredPosition != iconPosition)
        {
            Vector2 dir = (iconPosition - itemIcon.rectTransform.anchoredPosition);
            itemIcon.rectTransform.anchoredPosition += dir.normalized;
            if (Vector2.Distance(itemIcon.rectTransform.anchoredPosition, iconPosition) < 1)
            {
                itemIcon.rectTransform.anchoredPosition = iconPosition;
                itemIcon.GetComponent<InventoryItemScript>().isValid = true;    
                break;
            }
            yield return new WaitForSeconds(GameObject.FindFirstObjectByType<AddToInventory>().iconMoveSpeed);
        }

    }
}
