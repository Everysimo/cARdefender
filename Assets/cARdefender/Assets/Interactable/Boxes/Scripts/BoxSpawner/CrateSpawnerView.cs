using System;
using System.Collections;
using System.Collections.Generic;
using cARdefender.Tests.BoxPlacement;
using RMC.Core.Architectures.Mini.Context;
using RMC.Core.Architectures.Mini.View;
using UnityEngine;
using UnityEngine.Events;

public class BusDetectedEvent : UnityEvent<BoxConsumerHandle>{}

public class CrateSpawnerView : MonoBehaviour, IView
{
    //  Events ----------------------------------------
    [HideInInspector] public readonly BusDetectedEvent OnBusDetectedEvent = new BusDetectedEvent();
    
    private Coroutine _coroutine;

    [SerializeField]
    public BoxManager boxManager;

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

    //  Methods ---------------------------------------


    public void OnBusDected(BoxConsumerHandle boxConsumerHandle)
    {
        RequireIsInitialized();
        OnBusDetectedEvent.Invoke(boxConsumerHandle);
    }

    //  Event Handlers --------------------------------
}
