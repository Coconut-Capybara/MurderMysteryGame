using UnityEngine;

public class SafeButtonScript : NoItemInteractions
{
    [SerializeField] private int num;
    [SerializeField] private SafeOpenScript safe;

    public override void NoItemFunciton()
    {
        safe.UpdateButton(num); 
    }
   
}
