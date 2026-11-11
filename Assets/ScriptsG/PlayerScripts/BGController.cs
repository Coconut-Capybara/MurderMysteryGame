/*****************************************************************************
// Script Name : BGController.cs
// Author : Bryson Welch
// Additional Author(s) :
// Creation Date: 11/11/26
// Last Modified Date: 11/11/26
//
// Summary : Controls the background music in the interior scene
*****************************************************************************/
using UnityEngine;

public class BGController : MonoBehaviour
{
    [SerializeField] private AudioSource bGMusicController;
    [SerializeField] private AudioClip interiorMusicClip;
    [SerializeField] private AudioClip resultsBeginningClip;
    [SerializeField] private AudioClip resultsEndingClip;
    [SerializeField] private double resultsEndTime;
    [SerializeField] private double resultsLoopEndTime;
    public void ResultsMusic()
    {
        bGMusicController.Stop();
        if(bGMusicController.clip == interiorMusicClip)
        {
            bGMusicController.clip = resultsBeginningClip;
            bGMusicController.SetScheduledEndTime(resultsEndTime);
        }
        else if(bGMusicController.clip == resultsBeginningClip)
        {
            bGMusicController.clip = resultsEndingClip;
            bGMusicController.SetScheduledEndTime(resultsLoopEndTime);
        }
        bGMusicController.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if(bGMusicController.clip == resultsBeginningClip && bGMusicController.time >= resultsEndTime)
        {
            ResultsMusic();
        }
    }
}
