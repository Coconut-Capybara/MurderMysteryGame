/*****************************************************************************
// Script Name : DarkAreaScript
// Author : Bryson Welch
// Additional Author(s) :
// Creation Date: 10/3/26
// Last Modified Date: 10/3/26
//
// Summary : Checks if the player has the flashlight. And dets rid of dark areas accordingly
*****************************************************************************/
using UnityEngine;

public class DarkAreaScript : MonoBehaviour
{
    [SerializeField] private bool hasFlashlight;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        hasFlashlight = false;
    }

    /// <summary>
    /// Checks if the player has a flashlight
    /// </summary>
    public void FlashlightCheck()
    {
        if (hasFlashlight)
        {
            Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        FlashlightCheck();
    }
}
