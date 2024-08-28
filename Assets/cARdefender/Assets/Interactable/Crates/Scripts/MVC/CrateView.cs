using System;
using System.Collections;
using System.Collections.Generic;
using cARdefender.Assets.Enemies.Generic_Enemies.Scripts;
using cARdefender.Tests.BoxPlacement;
using RMC.Core.Architectures.Mini.Context;
using RMC.Core.Architectures.Mini.View;
using UnityEngine;
using UnityEngine.Events;


public class HealthCrateHittedUnityEvent : UnityEvent<float>{}
public class DoubleGunCrateHittedUnityEvent : UnityEvent<int>{}

public class AutoAimCrateHittedUnityEvent : UnityEvent<int>{}
public class CrateView : MonoBehaviour,IView,IHittableObject
{
     private Coroutine _coroutine;
     
     [HideInInspector] public readonly HealthCrateHittedUnityEvent OnHealthCrateHittedEvent = new HealthCrateHittedUnityEvent();
     [HideInInspector] public readonly DoubleGunCrateHittedUnityEvent OnDoubleGunCrateHittedEvent = new DoubleGunCrateHittedUnityEvent();
     [HideInInspector] public readonly AutoAimCrateHittedUnityEvent OnAutoAimCrateHittedEvent = new AutoAimCrateHittedUnityEvent();

     [SerializeField] private GameObject PickupPrefab;

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

    IContext IInitializableWithContext.Context => Context;

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

    public virtual void OnObjectHitted()
    {
        if (PickupPrefab)
        {
            Instantiate(PickupPrefab, transform.position, Quaternion.identity);
        }
    }

    public void DestroyBox()
    {
        Destroy(gameObject);
    }
    
    public void DisableBox()
    {
        gameObject.SetActive(false);
    }


    //  Event Handlers --------------------------------
    
}
