/*****************************************************************************
// Script Name : TimeController
// Author : Bryson Welch
// Additional Author(s) : Gabriel Andrews
// Creation Date: 9/6/26
// Last Modified Date: 9/18/26
//
// Summary : Acts as the middle man that adjusts existing time, adjusts its format, and puts it in in the UI
*****************************************************************************/
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using static UnityEngine.Rendering.DebugUI;

public class TimeController : MonoBehaviour
{
    [Header("Time Variables")]
    [Tooltip("The time remaining")]
    public static int timeLeft;
    [Tooltip("The amount of hours left. Do not manually adjust.")]
    [SerializeField] private int hoursLeft;
    [Tooltip("The amount of leftover minutes after hoursLeft has been calculated. Do not manually adjust this")]
    [SerializeField] private int minutesLeft;
    [Tooltip("Whether this is a build or not. True means that you can manually set the time in the scene.")]
    [SerializeField] private bool isTesting;
    [Tooltip("The time left if isTesting is set to true.")]
    [SerializeField] private int debugTimeLeft;

    [Header("Time UI Elements")]
    [Tooltip("The UI Panel that holds the Time Passing Check stuff")]
    [SerializeField] private GameObject timePanel;
    [Tooltip("The text that shares the info of what is happening, the time performing the action will take, and how much time will remain.")]
    [SerializeField] private TMP_Text timeAwayText;
    [Tooltip("The Confirm Button when performing a time-consuming action")]
    [SerializeField] private UnityEngine.UI.Button yesButton;
    [Tooltip("The Cancel Button when performing a time-consuming action")]
    [SerializeField] private UnityEngine.UI.Button noButton;

    [Tooltip("The UI Panel for the persistent timer in the top right corner")]
    [SerializeField] private GameObject timerPanel;
    [Tooltip("The text that holds the persistent time left info in the top right corner")]
    [SerializeField] private TMP_Text timerText;
    [Tooltip("Whether the timePanel game object is active or not")]
    [SerializeField] private bool timeUsePanel;

    private int tempTime;
    public int index;

    [Header("Object Properties Script")]
    [Tooltip("The objectProperties script of the object currently being interacted with. Do not manually Assign.")]
    [SerializeField] private ObjectProperties objectProperties;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //TimeAway(timeTestAway);
        if (isTesting)
        {
            timeLeft = debugTimeLeft;
        }
        timeUsePanel = false;
        timePanel.SetActive(false);
        TimeFormat();
        if(minutesLeft == 0)
        {
            timerText.text = hoursLeft.ToString() + ":00 left";
        }
        else
        {
            timerText.text = hoursLeft.ToString() + ":" + minutesLeft.ToString() + " left";
        }
        
    }
    /// <summary>
    /// Opens the timePanel and adjusts the text to the corresponding item use description, the time to be taken away, 
    /// and the time remaining after use
    /// </summary>
    /// <param name="timeUse"></param>
    /// <param name="item"></param>
    public void PassTimePanel(GameObject item, GameObject playerItem)
    {
        int timeUse;
        if(playerItem == null)
        {
            timeUse = item.GetComponent<ObjectProperties>().timeUsage[0];
        }
        else
        {
            timeUse =
                item.GetComponent<ObjectProperties>().timeUsage[GetTime(item
                .GetComponent<ItemNeeded>().itemNeeded, playerItem.GetComponent<InventoryItemScript>().GetItemId())];
        }
            timeUsePanel = true;
        timePanel.gameObject.SetActive(true);
        timeAwayText.text = (item.GetComponent<ObjectProperties>().timeText + 
            "\nTime taken away is: " + timeUse + " minutes" + "\n"  + "You will have " + 
            ((timeLeft-timeUse) / 60) + " hours and " + ((timeLeft-timeUse) % 60) + " minutes left.");
        tempTime = timeUse;

    }
    private int GetTime(GameObject[] items,int itemId)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i].GetComponent<InventoryItemScript>().GetItemId() == itemId)
            {
                index = i;  
                return i;
            }
        }
        return 0;
    }
    public void YesButton()
    {
        TimeAway(tempTime);
        timeUsePanel = false;
        timePanel.SetActive(false);
    }

    public void NoButton()
    {
        timeUsePanel = false;
        timePanel.gameObject.SetActive(false);
        GameObject.FindFirstObjectByType<PlayerInteract>().SetInPrompt(false);    
    }
    public void TimeAway(int time)
    {
        timeLeft = timeLeft - time;
        TimeFormat();
    }


    private void TimeFormat()
    {
        hoursLeft = timeLeft / 60;
        minutesLeft = timeLeft % 60;
        timerText.text = hoursLeft.ToString() + ":" + minutesLeft.ToString() + " left";
        TimeOutCheck();
    }

    private void TimeOutCheck()
    {
        if(timeLeft <= 0)
        {
            print("game is over.");
        }
    }
    public void SetTimeUsePanel(bool panel)
    {
        timeUsePanel = panel;
    }
    public GameObject GetTimePanel()
    {
        return timePanel;       
    }
    // Update is called once per frame
    void Update()
    {
        if (timeUsePanel)
        {
            timerPanel.SetActive(false);
        }
        else
        {
            timerPanel.SetActive(true);
        }
    }
}
