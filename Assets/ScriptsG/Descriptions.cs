using UnityEngine;

public class Descriptions : MonoBehaviour
{
    [SerializeField] private string description;

    public string GetDescription()
    {
        return description;     
    }
}
