using Unity.Cinemachine;
using UnityEngine;

public class MovementController : MonoBehaviour
{

    [SerializeField] private Camera m_Camera;
    [SerializeField] private CinemachineCamera m_CinemachineCamera;
    [SerializeField] private CinemachineSplineDolly splineDolly;
    [SerializeField] private int knotIndex;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        splineDolly = FindAnyObjectByType<CinemachineCamera>().GetComponent<CinemachineSplineDolly>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
