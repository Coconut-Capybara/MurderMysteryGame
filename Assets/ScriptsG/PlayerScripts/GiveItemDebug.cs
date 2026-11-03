using UnityEngine;

public class GiveItemDebug : MonoBehaviour
{
    [SerializeField] private GameObject[] itemsOnStart;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for(int i = 0; i < itemsOnStart.Length; i++)
        {
            GameObject.FindFirstObjectByType<AddToInventory>().AddNonWorldItem(itemsOnStart[i]);
        }
    }
}
