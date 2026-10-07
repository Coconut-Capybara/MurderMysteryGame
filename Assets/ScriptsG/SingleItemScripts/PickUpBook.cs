using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PickUpBook : MonoBehaviour
{
    [SerializeField] private GameObject[] currentBookOrder;
    [SerializeField] private GameObject[] bookOrderNeeded;
    [SerializeField] private GameObject puzzlePanel;
    [SerializeField] private int timeAway;
    InputAction grab;
    public GameObject currentBook;
    public bool locked;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        grab = InputSystem.actions.FindAction("Interact");  
    }
    public void Confirm()
    {
        GameObject.FindAnyObjectByType<TimeController>().TimeAway(timeAway);
        if (OrderValid())
        {
            print("Yes");
        }
        else
        {
            print("No");
        }
    }
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
    public void SwapBookOrder(int book1,int book2)
    {
        GameObject temp = currentBookOrder[book1];
        
        currentBookOrder[book1] = currentBookOrder[book2];  
        currentBookOrder[book2] = temp;

        int tempVal = currentBookOrder[book1].GetComponent<BookScript>().value;
        currentBookOrder[book1].GetComponent<BookScript>().value = currentBookOrder[book2]
            .GetComponent<BookScript>().value;
        currentBookOrder[book2].GetComponent<BookScript>().value = tempVal;
    }
    // Update is called once per frame
    void Update()
    {
        if (grab.WasPressedThisFrame())
        {
            if(currentBook != null && !locked)
            {
                currentBook.GetComponent<BookScript>().grabbed = true;
                locked = true;
            }
        }
        if(grab.WasReleasedThisFrame())
        {
            if(currentBook != null)
            {
                locked = false;
                currentBook.GetComponent<BookScript>().grabbed = false;
                currentBook.GetComponent<BookScript>().ReturnToPos();
                currentBook = null;
            }
        }
    }
}
