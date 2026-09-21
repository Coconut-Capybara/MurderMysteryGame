/*****************************************************************************
// Script Name : Descriptions
// Author : Gabriel Andrews
// Additional Author(s) :
// Creation Date:9/19/26
// Last Modified Date: 9/19/26
//
// Summary : holds the descriptions of each item
*****************************************************************************/
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
