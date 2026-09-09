using NUnit.Framework;
using Unity.Cinemachine;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    [SerializeField] private CinemachineCamera currentCam;
    [SerializeField] private CinemachineCamera firstCam;

    [SerializeField] private CinemachineBrain camBrain;
    [SerializeField] private Camera mainCam;
    [SerializeField] private CinemachineCamera[] allCams;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ClearCams();
        currentCam = firstCam;
        SetCamera();
        
    }

    private void ClearCams()
    {
        for(int i = 0; i < allCams.Length; i++)
        {
            allCams[i].enabled = false;
        }
    }

    private void SetCamera()
    {
        for (int i = 0; i < allCams.Length; i++)
        {
            if (allCams[i] == currentCam)
            {
                allCams[i].enabled = true;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
