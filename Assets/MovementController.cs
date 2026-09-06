using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Splines;

public class MovementController : MonoBehaviour
{
    [SerializeField] private SplineContainer dollySpline;
    [SerializeField] private Camera m_Camera;
    [SerializeField] private CinemachineCamera m_CinemachineCamera;
    [SerializeField] private CinemachineSplineDolly splineDolly;
    [SerializeField] private SplineContainer currentSpline;
    [SerializeField] private SplineSettings m_SplineSettings;
    public SplineContainer nextSpline;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_SplineSettings = dollySpline.GetComponent<SplineSettings>();
        currentSpline = m_SplineSettings.Spline;
    }

    // Update is called once per frame
    void Update()
    {
        if (nextSpline != null)
        {
            currentSpline = nextSpline;
        }
    }
}