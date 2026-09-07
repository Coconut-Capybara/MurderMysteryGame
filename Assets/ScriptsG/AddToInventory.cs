using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AddToInventory : MonoBehaviour
{
    [SerializeField] private Vector2 currentIconPos;
    [SerializeField] private Vector2 iconPivot;
    public float iconMoveSpeed;
    /// <summary>
    /// Puts item in inventory
    /// </summary>
    public void AddItem(GameObject item)
    {
        item.GetComponent<GrabbableObject>().iconPos = currentIconPos;
        StartCoroutine(item.GetComponent<GrabbableObject>().MoveIcon());
        currentIconPos += iconPivot;  
    }
}
