using UnityEngine;

public class TaxidermyPuzzle : NoItemInteractions
{
    [SerializeField] private float pivot;
    [SerializeField] private int timeAway;
    [SerializeField] private int[] headOrder;
    [SerializeField] private int[] orderNeeded;
    [SerializeField] private GameObject[] heads;
    public Vector3 headPos;
    private Vector3 headPosOrgin;
    public int headCount;

    private void Start()
    {
        headPosOrgin = headPos;
    }
    public void UpdateValues(int headValue)
    {
        headOrder[headCount] = headValue;
        headCount++;
        headPos.x += pivot;       
    }
    public void ResetPuzzle()
    {
        headCount = 0;
        headPos = headPosOrgin;
        for(int i = 0; i < headOrder.Length; i++) 
        {
            headOrder[i] = 0;
        }
        for(int i = 0; i < heads.Length; i++)
        {
            heads[i].transform.position = heads[i].GetComponent<TaxidermyHeadScript>().orgin;
            heads[i].GetComponent<TaxidermyHeadScript>().locked = false;
        }
    }
    public override void NoItemFunciton()
    {
        PuzzleConfirmation();
    }
    public void PuzzleConfirmation()
    {
        if (PuzzleCheck())
        {
            print("yes");
        }
        else
        {
            print("No");
        }
        GameObject.FindAnyObjectByType<TimeController>().TimeAway(timeAway);
    }   
    private bool PuzzleCheck()
    {
        bool valid = true;
        for (int i = 0; i < orderNeeded.Length; i++)
        {
            if (orderNeeded[i] != headOrder[i])
            {
                valid = false;
            }
        }
        return valid;
    }
}

