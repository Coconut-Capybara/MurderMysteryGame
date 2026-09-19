using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using static UnityEngine.Rendering.DebugUI;

public class TimeController : MonoBehaviour
{
    [Header("Time Variables")]
    [SerializeField] private int timeLeft;
    [SerializeField] private int hoursLeft;
    [SerializeField] private int minutesLeft;

    [Header("Time UI Elements")]
    public GameObject timePanel;
    [SerializeField] private TMP_Text timeAwayText;
    [SerializeField] private UnityEngine.UI.Button yesButton;
    [SerializeField] private UnityEngine.UI.Button noButton;
    [SerializeField] private GameObject timerPanel;
    [SerializeField] private TMP_Text timerText;

    public bool timeUsePanel;

    private int tempTime;

    [Header("Object Properties Script")]
    [SerializeField] private ObjectProperties objectProperties;

    [Header("Test Variables")]
    public int timeTestAway;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //TimeAway(timeTestAway);
        timeUsePanel = false;
        timePanel.SetActive(false);
        TimeFormat();
        timerText.text = hoursLeft.ToString() + ":" + minutesLeft.ToString() + " left";
    }

    public void PassTimePanel(int timeUse)
    {
        timeUsePanel = true;
        timePanel.gameObject.SetActive(true);
        timeAwayText.text = ("time taken away is: " + timeUse + " minutes" + "\n"  + "You will have " + 
            ((timeLeft-timeUse) / 60) + " hours and " + ((timeLeft-timeUse) % 60) + " minutes left.");
        tempTime = timeUse;
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
