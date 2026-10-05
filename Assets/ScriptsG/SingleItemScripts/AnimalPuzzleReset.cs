using UnityEngine;

public class AnimalPuzzleReset : NoItemInteractions
{
    public override void NoItemFunciton()
    {
        GameObject.FindFirstObjectByType<TaxidermyPuzzle>().ResetPuzzle();      
    }
}
