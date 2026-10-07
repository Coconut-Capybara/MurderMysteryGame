using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class BookScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public int value;
    public bool grabbed;
    private PlayerInteract player;
    private PickUpBook pickUp;
    public Vector2 curPos;
    public GameObject targetBook;
    private bool overBook;

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        if(pickUp.currentBook == null && !pickUp.locked)
        {
            pickUp.currentBook = gameObject;
        }
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        if (!pickUp.locked)
        {
            pickUp.currentBook = null;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindFirstObjectByType<PlayerInteract>();  
        pickUp = GameObject.FindFirstObjectByType<PickUpBook>();
    }

    // Update is called once per frame
    void Update()
    {
        if (grabbed)
        {
            Vector2 mousePosition = Mouse.current.position.ReadValue();
            GetComponent<RectTransform>().position = mousePosition;
        }
    }
    public void SwapPos(GameObject target)
    {
        target.GetComponent<RectTransform>().anchoredPosition = curPos;
        GetComponent<RectTransform>().anchoredPosition = target.GetComponent<BookScript>().curPos;
        curPos = target.GetComponent<BookScript>().curPos;
        target.GetComponent<BookScript>().curPos = target.GetComponent<RectTransform>().anchoredPosition;
        pickUp.SwapBookOrder(value, targetBook.GetComponent<BookScript>().value);
    }
    public void ReturnToPos()
    {
        if (!overBook)
        {
            GetComponent<RectTransform>().anchoredPosition = curPos;
        }
        else
        {
            SwapPos(targetBook);        
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 6)
        {
            targetBook = collision.gameObject;
            overBook = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            targetBook = null;
            overBook = false;   
        }
    }
}
