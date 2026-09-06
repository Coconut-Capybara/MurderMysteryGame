using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Splines;

public class MovementController : MonoBehaviour
{
    [SerializeField] private SplineContainer dollySpline;
    [SerializeField] private Camera m_Camera;
    [SerializeField] private CinemachineCamera m_CinemachineCamera;
    [SerializeField] private CinemachineSplineDolly splineDolly;
    [SerializeField] private SplineContainer currentSpline;
    [SerializeField] private SplineSettings m_SplineSettings;
    [SerializeField] private MovableObject movableObject;
    [SerializeField] private GameObject[] gameObjects;
    public int localLocationIndex;
    public SplineContainer nextSpline;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //m_SplineSettings = dollySpline.GetComponent<SplineSettings>();
        currentSpline = m_SplineSettings.Spline;
        localLocationIndex = 0;
    }

    private void InteractRay()
    {
        Vector3 cursorPos = Mouse.current.position.ReadValue();
        Ray ray = m_Camera.ScreenPointToRay(cursorPos);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject.GetComponent<MovableObject>() != null)
            {
                if ((localLocationIndex > hit.collider.gameObject.GetComponent<MovableObject>().locationIndexGlobal)
                    && (hit.collider.gameObject.GetComponent<MovableObject>().locationIndexGlobal == 0))
                {
                    nextSpline = hit.collider.gameObject.GetComponent<MovableObject>().leftSpline;
                    localLocationIndex = hit.collider.gameObject.GetComponent<MovableObject>().locationIndexGlobal;
                }
                else if (localLocationIndex < hit.collider.gameObject.GetComponent<MovableObject>().locationIndexGlobal)
                {
                    nextSpline = hit.collider.gameObject.GetComponent<MovableObject>().rightSpline;
                    localLocationIndex = hit.collider.gameObject.GetComponent<MovableObject>().locationIndexGlobal;
                }
                else
                {
                    nextSpline = hit.collider.gameObject.GetComponent<MovableObject>().leftSpline;
                    localLocationIndex = hit.collider.gameObject.GetComponent<MovableObject>().locationIndexGlobal;
                }
                    
            }
            else
            {
                print("Not assigned");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        InteractRay();
        if (nextSpline != null)
        {
            currentSpline = nextSpline;
        }
    }
}