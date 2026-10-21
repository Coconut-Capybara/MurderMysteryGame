using UnityEngine;

public class UpdateLoungeHeads : MonoBehaviour
{
    [SerializeField] private GameObject[] loungeHeads;
    [SerializeField] private Sprite wolf, deer, boar,wolfH,deerH,boarH;
    public void UpdateHeads(int[] headOrder)
    {
        for(int i = 0; i < loungeHeads.Length; i++)
        {
            UpdateSprite(loungeHeads[i], headOrder[i]);
        }
    }
    private void UpdateSprite(GameObject go,int index)
    {
        if(index == 1)
        {
            go.GetComponent<SpriteRenderer>().sprite = wolf;
            go.GetComponent<ObjectProperties>().itemSprite = wolf;
            go.GetComponent<ObjectProperties>().highlightSprite = wolfH;
        }
        else if (index == 2)
        {
            go.GetComponent<SpriteRenderer>().sprite = deer;
            go.GetComponent<ObjectProperties>().itemSprite = deer;
            go.GetComponent<ObjectProperties>().highlightSprite = deerH;
        }
        else if (index == 3)
        {
            go.GetComponent<SpriteRenderer>().sprite = boar;
            go.GetComponent<ObjectProperties>().itemSprite = boar;
            go.GetComponent<ObjectProperties>().highlightSprite = boarH;
        }
    }
}
