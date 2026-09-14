using NUnit.Framework;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementController : MonoBehaviour
{
    [SerializeField] private CinemachineCamera currentCam;
    [SerializeField] private CinemachineCamera firstCam;

    InputAction interact;

    [SerializeField] private CinemachineBrain camBrain;
    [SerializeField] private Camera mainCam;
    [SerializeField] private CinemachineCamera[] allCams;

    private MovePointData movePointData;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ClearCams();
        currentCam = firstCam;
        SetCamera();
        interact = InputSystem.actions.FindAction("Interact");
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

    private void GetCamera(GameObject camSpot)
    {
        movePointData = camSpot.gameObject.GetComponent<MovePointData>();
        currentCam = movePointData.cineCam;
    }

    private void InteractRay()
    {
        if (interact.WasPressedThisFrame())
        {
            Vector3 cursorPos = Mouse.current.position.ReadValue();
            Ray ray = mainCam.ScreenPointToRay(cursorPos);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.GetComponent<MovePointData>() != null)
                {
                    GetCamera(hit.collider.gameObject);
                    ClearCams();
                    SetCamera();
                }
                else
                {
                    print("Not a valid Move Point");
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        InteractRay();
    }
}
