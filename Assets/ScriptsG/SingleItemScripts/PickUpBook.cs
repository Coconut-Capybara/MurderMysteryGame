/*****************************************************************************
// Script Name : PickUpBook
// Author : Gabriel Andrews
// Additional Author(s) :
// Creation Date:10/07/26
// Last Modified Date: 10/14/26
//
// Summary : Allows the player to pick up books in the bookshelf puzzle
*****************************************************************************/
using UnityEngine;
using UnityEngine.InputSystem;

public class PickUpBook : MonoBehaviour
{
    [SerializeField] private GameObject[] currentBookOrder;
    [SerializeField] private GameObject[] bookOrderNeeded;
    [SerializeField] private GameObject puzzlePanel;
    [SerializeField] private GameObject bookShelf;
    InputAction grab;
    private GameObject currentBook;
    private bool locked;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        grab = InputSystem.actions.FindAction("Interact");  
    }
    /// <summary>
    /// Checks if the player has the books in the correct order, 
    /// takes time regardless
    /// </summary>
    public void Confirm()
    {
        if (OrderValid())
        {
            print("Yes");
        }
        else
        {
            bookShelf.GetComponent<ObjectProperties>().timeUsage = 10;
            bookShelf.GetComponent<ObjectProperties>().timeLock = false;
        }
        GameObject.FindFirstObjectByType<StartBookPuzzle>().ClosePuzzle();      
    }
    
    /// <summary>
    /// Returns true if the book order is correct, else returns false
    /// </summary>
    /// <returns></returns>
    private bool OrderValid()
    {
        bool valid = true;
        for (int i = 0; i < bookOrderNeeded.Length; i++)
        {
            if (bookOrderNeeded[i] != currentBookOrder[i])
            {
                valid = false;
            }
        }
        return valid;
    }
    /// <summary>
    /// Swaps two books position in the array
    /// </summary>
    /// <param name="book1"></param>
    /// <param name="book2"></param>
    public void SwapBookOrder(int book1,int book2)
    {
        GameObject temp = currentBookOrder[book1];
        
        currentBookOrder[book1] = currentBookOrder[book2];  
        currentBookOrder[book2] = temp;

        int tempVal = currentBookOrder[book1].GetComponent<BookScript>().GetValue();
        currentBookOrder[book1].GetComponent<BookScript>().SetValue(currentBookOrder[book2]
            .GetComponent<BookScript>().GetValue());
        currentBookOrder[book2].GetComponent<BookScript>().SetValue(tempVal);       
    }
    // Update is called once per frame
    void Update()
    {
        if (grab.WasPressedThisFrame())
        {
            if(currentBook != null && !locked)
            {
                currentBook.GetComponent<BookScript>().SetGrabbed(true);
                locked = true;
            }
        }
        if(grab.WasReleasedThisFrame())
        {
            if(currentBook != null)
            {
                locked = false;
                currentBook.GetComponent<BookScript>().SetGrabbed(false);
                currentBook.GetComponent<BookScript>().ReturnToPos();
                currentBook = null;
            }
        }
    }
    public GameObject GetCurrentBook()
    {
        return currentBook;
    }
    public void SetCurrentBook(GameObject _currentBook)
    {
        currentBook = _currentBook; 
    }
    public bool GetLockState()
    {
        return locked;          
    }
}
