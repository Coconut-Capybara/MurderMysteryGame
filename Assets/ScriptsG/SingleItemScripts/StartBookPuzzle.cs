using UnityEngine;

public class StartBookPuzzle : NoItemInteractions
{
    [SerializeField] private GameObject bookPuzzlePanel;
    public override void NoItemFunciton()
    {
        GameObject.FindFirstObjectByType<PlayerInteract>().SetInPrompt(true); 
        bookPuzzlePanel.SetActive(true);
    }
    public void ClosePuzzle()
    {
        bookPuzzlePanel.SetActive(false);
        GameObject.FindFirstObjectByType<PlayerInteract>().SetInPrompt(false);
    }
}
