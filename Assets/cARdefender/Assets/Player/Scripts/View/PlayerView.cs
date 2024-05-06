using System;
using System.Collections;
using System.Collections.Generic;
using RMC.Core.Architectures.Mini.Context;
using RMC.Core.Architectures.Mini.Samples.RollABall.WithMini.Components;
using RMC.Core.Architectures.Mini.Samples.RollABall.WithMini.Mini.Controller.Commands;
using RMC.Core.Architectures.Mini.View;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

//  Namespace Properties ------------------------------

//  Class Attributes ----------------------------------
public class PlayerHittedUnityEvent : UnityEvent<float>{}

public class InitializePlayerEvent : UnityEvent<float>{}


/// <summary>
/// The View handles user interface and user input
/// </summary>
public class PlayerView : MonoBehaviour, IView, IHittableEnemy
{
    //  Events ----------------------------------------
    [HideInInspector] public readonly PlayerHittedUnityEvent OnPlayerHitted = new PlayerHittedUnityEvent();
    [HideInInspector] public readonly InitializePlayerEvent OnInitializePlayerEvent = new InitializePlayerEvent();
    
    private Coroutine _coroutine;

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
    
    [SerializeField] 
    private float playerLife;
    


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

    private void Start()
    {
        OnInitializePlayerEvent.Invoke(playerLife);
    }

    public void OnTakeDamage(float damage)
    {
       OnPlayerHitted.Invoke(damage);
    }
    


    //  Unity Methods ---------------------------------
    

    private void OnDisable()
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
            _coroutine = null;
        }
    }
    

    //  Methods ---------------------------------------



    //  Event Handlers --------------------------------
    
}