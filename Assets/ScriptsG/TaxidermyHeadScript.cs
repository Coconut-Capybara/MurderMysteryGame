using UnityEngine;

public class TaxidermyHeadScript : NoItemInteractions
{
    [SerializeField] private TaxidermyPuzzle puzzle;
    public Vector3 orgin;
    public int value;
    public bool locked;
    public override void NoItemFunciton()
    {
        if(puzzle.headCount < 3 && !locked)
        {
            transform.position = puzzle.headPos;
            puzzle.UpdateValues(value);  
            locked = true;
        }
    }
}
