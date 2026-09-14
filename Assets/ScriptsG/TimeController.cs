using UnityEngine;

public class TimeController : MonoBehaviour
{
    [SerializeField] private int timeLeft;
    [SerializeField] private int hoursLeft;
    [SerializeField] private int minutesLeft;
    public int timeTestAway;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        TimeAway(timeTestAway);
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
