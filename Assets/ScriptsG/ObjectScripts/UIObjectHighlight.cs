using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIObjectHighlight : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private ObjectProperties properties;
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!properties.usesAltSprites)
        {
            GetComponent<Image>().sprite = properties.highlightSprite;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!properties.usesAltSprites)
        {
            GetComponent<Image>().sprite = properties.itemSprite;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        properties = GetComponent<ObjectProperties>();  
    }
}
