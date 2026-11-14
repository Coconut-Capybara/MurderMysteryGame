/*****************************************************************************
// Script Name : MovePointData
// Author : Bryson Welch
// Additional Author(s) :
// Creation Date: 9/9/26
// Last Modified Date: 11/14/26
//
// Summary : Holds data related to the move point clicked on
*****************************************************************************/
using Unity.Cinemachine;
using UnityEngine;

public class MovePointData : MonoBehaviour
{
    [SerializeField] private CinemachineCamera cineCam;
    public bool usesAltSound;
    public AudioClip altAudioClip;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public CinemachineCamera GetCamera()
    {
        return cineCam; 
    }
}
