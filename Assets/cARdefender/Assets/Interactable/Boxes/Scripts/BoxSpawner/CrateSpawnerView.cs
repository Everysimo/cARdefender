using System;
using System.Collections;
using System.Collections.Generic;
using cARdefender.Tests.BoxPlacement;
using RMC.Core.Architectures.Mini.Context;
using RMC.Core.Architectures.Mini.View;
using UnityEngine;
using UnityEngine.Events;

public class HealthBoxSpawnRequestEvent : UnityEvent<BoxConsumerHandle>{}
public class DoubleGunBoxSpawnRequestEvent : UnityEvent<BoxConsumerHandle>{}

public class SpawnCrateRequestEvent : UnityEvent{}

public class CrateSpawnerView : MonoBehaviour, IView
{
    //  Events ----------------------------------------
    [HideInInspector] public readonly HealthBoxSpawnRequestEvent OnHealthBoxSpawnRequestEvent = new HealthBoxSpawnRequestEvent();
    [HideInInspector] public readonly DoubleGunBoxSpawnRequestEvent OnDoubleGunBoxSpawnRequestEvent = new DoubleGunBoxSpawnRequestEvent();
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
    
    public void HealthBoxSpawnRequest(BoxConsumerHandle boxConsumerHandle)
    {
        RequireIsInitialized();
        OnHealthBoxSpawnRequestEvent.Invoke(boxConsumerHandle);
    }
    
    public void DoubleGunBoxSpawnRequest(BoxConsumerHandle boxConsumerHandle)
    {
        RequireIsInitialized();
        OnDoubleGunBoxSpawnRequestEvent.Invoke(boxConsumerHandle);
    }
    
    public void DoubleGunBoxSpawnRequestNoArg()
    {
        RequireIsInitialized();
        OnSpawnCrateRequestEvent.Invoke();
    }

    //  Event Handlers --------------------------------
}
