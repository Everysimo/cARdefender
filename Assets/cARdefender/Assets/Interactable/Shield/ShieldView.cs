using System;
using System.Collections;
using System.Collections.Generic;
using cARdefender.Assets.Enemies.Generic_Enemies.Scripts;
using RMC.Core.Architectures.Mini.Context;
using RMC.Core.Architectures.Mini.View;
using UnityEngine;
using UnityEngine.Events;

public class ShieldHittedUnityEvent : UnityEvent{}

public class ShieldView : MonoBehaviour,IView,IHittableObject
{
    private Coroutine _coroutine;
     
    [HideInInspector] public readonly ShieldHittedUnityEvent OnShieldHittedEvent = new ShieldHittedUnityEvent();

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

    public void OnObjectHitted()
    {
        
    }
    

    //  Event Handlers --------------------------------
    
}
