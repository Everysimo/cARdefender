using System;
using System.Collections;
using System.Collections.Generic;
using RMC.Core.Architectures.Mini.Context;
using RMC.Core.Architectures.Mini.View;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerView : MonoBehaviour, IView
{
    //  Events ----------------------------------------
    
    private Coroutine _coroutine;

    public bool isPaused;

    private TimeSpan timePlaying;

    private bool isTimerGoing = false;

    public float elapsedTime;

    
    [SerializeField] 
    private TextMeshProUGUI scoreText;
    
    [SerializeField] 
    private TextMeshProUGUI timerText;
    
    [SerializeField] 
    private TextMeshProUGUI livesLostText;

    //  Properties ------------------------------------
    public void RequireIsInitialized()
    {
        if (!IsInitialized)
        {
            throw new Exception("MustBeInitialized");
        }
    }

    public bool IsInitialized
    {
        get { return _isInitialized; }
    }

    public IContext Context
    {
        get { return _context; }
    }


    //  Fields ----------------------------------------
    private bool _isInitialized = false;
    private IContext _context;

    //  Initialization  -------------------------------
    public void Initialize(IContext context)
    {
        if (!IsInitialized)
        {
            _isInitialized = true;
            _context = context;
            
            //
            
            //

        }
    }
    
    //  Unity Methods ---------------------------------
    void Start()
    {
        //isTimerGoing = false;
    }


    //  Methods ---------------------------------------
    public void UpdateScoreView(int newScore)
    {
        scoreText.text = ""+newScore;
    }
    
    public void UpdateTimerView(string newTime)
    {
        timerText.text = ""+newTime;
    }
    
    public void UpdateLivesLostView(int newLives)
    {
        livesLostText.text = ""+newLives;
    }
    
    //------TIMER------
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
            UpdateTimerView(timePlayingStr);
            yield return null;
        }
    }
    //  Event Handlers --------------------------------
    
}