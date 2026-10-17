/*****************************************************************************
// Script Name : BookScript
// Author : Gabriel Andrews
// Additional Author(s) :
// Creation Date:10/07/26
// Last Modified Date: 10/14/26
//
// Summary : Handles the logic of the books in the bookshelf puzzle
*****************************************************************************/
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;


public class BookScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
   [SerializeField] private int value;
   [SerializeField] private Vector2 curPos;
    private bool grabbed;
    private PlayerInteract player;
    private PickUpBook pickUp;
    [SerializeField] private GameObject targetBook;
    private bool overBook;

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        if(pickUp.GetCurrentBook() == null && !pickUp.GetLockState())
        {
            pickUp.SetCurrentBook(gameObject);
        }
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        if (!pickUp.GetLockState())
        {
            pickUp.SetCurrentBook(null);        
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
    /// <summary>
    /// Finds the target book, and swaps their position, and order in the list
    /// </summary>
    /// <param name="target"></param>
    public void SwapPos(GameObject target)
    {
        target.GetComponent<RectTransform>().anchoredPosition = curPos;
        GetComponent<RectTransform>().anchoredPosition = target.GetComponent<BookScript>().curPos;
        curPos = target.GetComponent<BookScript>().curPos;
        target.GetComponent<BookScript>().curPos = target.GetComponent<RectTransform>().anchoredPosition;
        pickUp.SwapBookOrder(value, targetBook.GetComponent<BookScript>().value);
    }
    /// <summary>
    /// Returns the current book to its position
    /// </summary>
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

    public int GetValue()
    {
        return value;   
    }
    public void SetValue(int _value)
    {
        value = _value;     
    }
    public void SetGrabbed(bool _grabbed)
    {
        grabbed = _grabbed;     
    }
}
