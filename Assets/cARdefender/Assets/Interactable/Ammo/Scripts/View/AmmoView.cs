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
public class CollisionWithObjectUnityEvent : UnityEvent{}
public class InitializeAmmoEvent : UnityEvent<float,float>{}


/// <summary>
/// The View handles user interface and user input
/// </summary>
public class AmmoView : MonoBehaviour, IView
{
    //  Events ----------------------------------------
    [HideInInspector] public readonly CollisionWithObjectUnityEvent OnCollisionWithObject = new CollisionWithObjectUnityEvent();
    [HideInInspector] public readonly InitializeAmmoEvent OnInitializeAmmoEvent = new InitializeAmmoEvent();

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
    private float shootDamage;
    
    [SerializeField]
    [Tooltip("The speed at which the projectile is launched")]
    float shootSpeed = 1.0f;


    

    //  Initialization  -------------------------------
    public void Initialize(IContext context)
    {
        if (!IsInitialized)
        {
            _isInitialized = true;
            _context = context;
            
            //
            OnInitializeAmmoEvent.Invoke(shootDamage,shootSpeed);
            
            //

            
            //

        }
    }
    
    public void CollsionWithObject()
    {
        OnCollisionWithObject.Invoke();
    }

    private void OnCollisionEnter(Collision other)
    {
        CollsionWithObject();
    }

    //  Unity Methods ---------------------------------
        
        
    protected void OnDestroy()
    {
       
    }


    //  Methods ---------------------------------------


    //  Event Handlers --------------------------------

}