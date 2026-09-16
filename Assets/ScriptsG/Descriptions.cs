using UnityEngine;

public class Descriptions : MonoBehaviour
{
    [SerializeField] private string description;

    public string GetDescription()
    {
        return description;     
    }
    public void SetDescription(string _description)
    {
        description = _description;     
    }
}
