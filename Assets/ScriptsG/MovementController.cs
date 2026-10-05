/*****************************************************************************
// Script Name : MovementController
// Author : Bryson Welch
// Additional Author(s) : Gabriel Andrews
// Creation Date: 9/9/26
// Last Modified Date: 9/30/26
//
// Summary : Handles Movement between cameras
*****************************************************************************/
using NUnit.Framework;
using System.Collections;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class MovementController : MonoBehaviour
{
    [Header("Camera Assignments")]
    [Tooltip("The current camera the player is at. DO NOT ASSIGN")]
    [SerializeField] private CinemachineCamera currentCam;
    [Tooltip("The first Camera to go to when the scene starts")]
    [SerializeField] private CinemachineCamera firstCam;

    InputAction interact;
    [Tooltip("The Cinemachine Brain")]
    [SerializeField] private CinemachineBrain camBrain;
    [Tooltip("The Unity Camera that will move around")]
    [SerializeField] private Camera mainCam;
    [Tooltip("The list of all Cinemachine Cams in the Scene the Player can go to")]
    [SerializeField] private CinemachineCamera[] allCams;

    private MovePointData movePointData;
    [Tooltip("The Panel that produces the fade effect when switching cams")]
    [SerializeField] private GameObject blackScreen;
    [Tooltip("The time between each fade increment. The lower the number, the faster it is")]
    [SerializeField] private float fadeSpeed;
    [Tooltip("The % value of the change in transparency of the blackScreen game object. " +
        "Please keep above zero, but below 1. /n  The larger the number, the less smooth the fade in/out")]
    [SerializeField] private float fadePivot;

    private bool locked;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ClearCams();
        currentCam = firstCam;
        //SetCamera();
        interact = InputSystem.actions.FindAction("Interact");
    }
    /// <summary>
    /// Disables all cameras
    /// </summary>
    private void ClearCams()
    {
        for(int i = 0; i < allCams.Length; i++)
        {
            allCams[i].enabled = false;
        }
    }
    /// <summary>
    /// Enables the one camera the player will go to
    /// </summary>
    private void SetCamera()
    {
        for (int i = 0; i < allCams.Length; i++)
        {
            if (allCams[i] == currentCam)
            {
                if (!locked)
                {
                    StartCoroutine(FadeInOut(i));
                }
            }
        }
    }
    /// <summary>
    /// Fades a UI object in and out to hide the sudden camera transition.
    /// </summary>
    /// <param name="i"> The one camera to enable to take the player to </param>
    /// <returns></returns>
    private IEnumerator FadeInOut(int i)
    {
        locked = true;
        float fadeValue = 0;
        Color screenColor;
        while(fadeValue <= 1)
        {
            fadeValue += fadePivot;
            screenColor = new Color(0, 0, 0, fadeValue);
            blackScreen.GetComponent<Image>().color = screenColor;      
            yield return new WaitForSeconds(fadeSpeed);
        }
        allCams[i].enabled = true;
        while (fadeValue >= 0)
        {
            fadeValue -= fadePivot;
            screenColor = new Color(0, 0, 0, fadeValue);
            blackScreen.GetComponent<Image>().color = screenColor;
            yield return new WaitForSeconds(fadeSpeed);
        }
        locked = false;
    }
    /// <summary>
    /// Obtains the data of the camera to move to
    /// </summary>
    /// <param name="camSpot"> The game object to obtain the data from. </param>
    private void GetCamera(GameObject camSpot)
    {
        movePointData = camSpot.gameObject.GetComponent<MovePointData>();
        currentCam = movePointData.cineCam;
    }

    /// <summary>
    /// Performs the camera switching functions when an object with the MovePointData script attached to it.
    /// </summary>
    private void InteractRay()
    {
        if (interact.WasPressedThisFrame() && !GameObject.FindFirstObjectByType<PlayerInteract>().
            inPrompt && GameObject.FindFirstObjectByType<PlayerInteract>().inGame)
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
                    //print("Not a valid Move Point");
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
