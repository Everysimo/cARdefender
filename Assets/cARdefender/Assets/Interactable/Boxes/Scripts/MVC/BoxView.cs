using System;
using System.Collections;
using System.Collections.Generic;
using cARdefender.Assets.Enemies.Generic_Enemies.Scripts;
using cARdefender.Tests.BoxPlacement;
using RMC.Core.Architectures.Mini.Context;
using RMC.Core.Architectures.Mini.View;
using UnityEngine;
using UnityEngine.Events;


public class BoxHittedUnityEvent : UnityEvent<float>{}
public class BoxView : MonoBehaviour,IView,IHittableObject
{
     private Coroutine _coroutine;
     
     [HideInInspector] public readonly BoxHittedUnityEvent OnBoxHittedEvent = new BoxHittedUnityEvent();

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

    [SerializeField]
    private float lifeToRecover;
    

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
        OnBoxHittedEvent.Invoke(lifeToRecover);
    }

    public void DestryBox()
    {
        Destroy(gameObject);
    }
    
    public void DisableBox()
    {
        gameObject.SetActive(false);
    }


    //  Event Handlers --------------------------------
    
}
