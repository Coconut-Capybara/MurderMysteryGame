using UnityEngine;

public class UpdateLoungeHeads : MonoBehaviour
{
    [SerializeField] private GameObject[] loungeHeads;
    [SerializeField] private Sprite wolf, deer, boar,wolfH,deerH,boarH;
    [SerializeField] private float[] yValuesBase;
    [SerializeField] private float[] yValuesHigh;
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
            if(go.GetComponent<MiddleTaxHead>() != null)
            {
                go.transform.position = new Vector3(go.transform.position.x, yValuesHigh[0], go.transform.position.z);
            }
            else
            {
                go.transform.position = new Vector3(go.transform.position.x, yValuesBase[0], go.transform.position.z);
            }
        }
        else if (index == 2)
        {
            go.GetComponent<SpriteRenderer>().sprite = deer;
            go.GetComponent<ObjectProperties>().itemSprite = deer;
            go.GetComponent<ObjectProperties>().highlightSprite = deerH;
            if (go.GetComponent<MiddleTaxHead>() != null)
            {
                go.transform.position = new Vector3(go.transform.position.x, yValuesHigh[1], go.transform.position.z);
            }
            else
            {
                go.transform.position = new Vector3(go.transform.position.x, yValuesBase[1], go.transform.position.z);
            }
        }
        else if (index == 3)
        {
            go.GetComponent<SpriteRenderer>().sprite = boar;
            go.GetComponent<ObjectProperties>().itemSprite = boar;
            go.GetComponent<ObjectProperties>().highlightSprite = boarH;
            if (go.GetComponent<MiddleTaxHead>() != null)
            {
                go.transform.position = new Vector3(go.transform.position.x, yValuesHigh[2], go.transform.position.z);
            }
            else
            {
                go.transform.position = new Vector3(go.transform.position.x, yValuesBase[2], go.transform.position.z);
            }
        }
    }
}
