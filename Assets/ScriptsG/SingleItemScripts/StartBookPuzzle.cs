using UnityEngine;

public class StartBookPuzzle : NoItemInteractions
{
    [SerializeField] private GameObject bookPuzzlePanel;
    [SerializeField] private bool bookPuzzleLocked;
    [SerializeField] private GameObject books;
    public override void NoItemFunciton()
    {
        if (!GameObject.FindFirstObjectByType<PlayerInteract>().GetInPrompt())
        {
            if (!bookPuzzleLocked)
            {
                GameObject.FindFirstObjectByType<FollowPlayerCursor>().ResetCube();
                GameObject.FindFirstObjectByType<PlayerInteract>().SetInPrompt(true);
                books.SetActive(false);
                bookPuzzlePanel.SetActive(true);
            }
            else
            {
                GameObject.FindFirstObjectByType<TextPrompts>().ShowPrompt("There appears to be a book missing");
            }
        }       
    }
    public void ClosePuzzle()
    {
        bookPuzzlePanel.SetActive(false);
        books.SetActive(true);
    }
    public void LockPuzzle()
    {
        bookPuzzleLocked = true;
    }
    public void UnlockPuzzle()
    {
        bookPuzzleLocked = false;
    }
}
