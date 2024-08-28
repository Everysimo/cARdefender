using System;
using System.Collections;
using System.Collections.Generic;
using cARdefender.Tests.BoxPlacement;
using RMC.Core.Architectures.Mini.Context;
using RMC.Core.Architectures.Mini.View;
using UnityEngine;
using UnityEngine.Events;

public class HealthCrateSpawnRequestEvent : UnityEvent<BoxConsumerHandle>{}
public class DoubleGunCrateSpawnRequestEvent : UnityEvent<BoxConsumerHandle>{}
public class AutoAimCrateSpawnRequestEvent : UnityEvent<BoxConsumerHandle>{}



public class SpawnCrateRequestEvent : UnityEvent{}

public class CrateSpawnerView : MonoBehaviour, IView
{
    //  Events ----------------------------------------
    [HideInInspector] public readonly HealthCrateSpawnRequestEvent OnHealthCrateSpawnRequestEvent = new HealthCrateSpawnRequestEvent();
    [HideInInspector] public readonly DoubleGunCrateSpawnRequestEvent OnDoubleGunCrateSpawnRequestEvent = new DoubleGunCrateSpawnRequestEvent();
    [HideInInspector] public readonly AutoAimCrateSpawnRequestEvent OnAutoAimCrateSpawnRequestEvent = new AutoAimCrateSpawnRequestEvent();
    [HideInInspector] public readonly SpawnCrateRequestEvent OnSpawnCrateRequestEvent = new SpawnCrateRequestEvent();

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
    
    public void HealthCrateSpawnRequest(BoxConsumerHandle boxConsumerHandle)
    {
        RequireIsInitialized();
        OnHealthCrateSpawnRequestEvent.Invoke(boxConsumerHandle);
    }
    
    public void DoubleGunCrateSpawnRequest(BoxConsumerHandle boxConsumerHandle)
    {
        RequireIsInitialized();
        OnDoubleGunCrateSpawnRequestEvent.Invoke(boxConsumerHandle);
    }
    
    public void AutoAimCrateSpawnRequest(BoxConsumerHandle boxConsumerHandle)
    {
        RequireIsInitialized();
        OnAutoAimCrateSpawnRequestEvent.Invoke(boxConsumerHandle);
    }
    
    public void DoubleGunBoxSpawnRequestNoArg()
    {
        RequireIsInitialized();
        OnSpawnCrateRequestEvent.Invoke();
    }

    //  Event Handlers --------------------------------
}
