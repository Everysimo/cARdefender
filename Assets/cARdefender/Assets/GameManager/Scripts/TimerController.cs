using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerController : MonoBehaviour
{
    public static TimerController instance;

    private TimeSpan timePlaying;

    private bool isTimerGoing;

    public float elapsedTime;

    private void Awake()
    {
        instance = this;
    }
    
    void Start()
    {
        isTimerGoing = false;
    }

    public void BeginTimer()
    {
        isTimerGoing = true;
        elapsedTime = 0f;
        StartCoroutine( UpdateTimer( ) ) ;
    }

    public void StopTimer()
    {
        isTimerGoing = false;
    }
    
    private IEnumerator UpdateTimer()
    {
        while (isTimerGoing)
        {
            elapsedTime += Time.deltaTime;
            timePlaying = TimeSpan.FromSeconds(elapsedTime);
            string timePlayingStr = timePlaying.ToString("mm':'ss'.'ff");
            yield return null;
        }
    }
}
