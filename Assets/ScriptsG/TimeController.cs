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
    [SerializeField] private GameObject timePanel;
    [SerializeField] private TMP_Text timeAwayText;
    [SerializeField] private UnityEngine.UI.Button yesButton;
    [SerializeField] private UnityEngine.UI.Button noButton;

    private int tempTime;

    [Header("Object Properties Script")]
    [SerializeField] private ObjectProperties objectProperties;

    [Header("Test Variables")]
    public int timeTestAway;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //TimeAway(timeTestAway);
        timePanel.SetActive(false);
    }

    public void PassTimePanel(int timeUse)
    {
        timePanel.gameObject.SetActive(true);
        timeAwayText.text = ("time taken away is: " + timeUse + " minutes" + "\n"  + "You will have " + 
            ((timeLeft-timeUse) / 60) + " hours and " + ((timeLeft-timeUse) % 60) + " minutes left.");
        tempTime = timeUse;
    }

    public void YesButton()
    {
        TimeAway(tempTime);
        timePanel.SetActive(false);
    }

    public void NoButton()
    {
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
        
    }
}
