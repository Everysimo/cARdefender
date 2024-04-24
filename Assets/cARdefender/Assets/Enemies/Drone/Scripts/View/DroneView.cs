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
public class DroneHittedUnityEvent : UnityEvent<float>{}
public class InitializeDroneEvent : UnityEvent<float,float,float,float>{}


/// <summary>
/// The View handles user interface and user input
/// </summary>
public class DroneView : MonoBehaviour, IView, IHittableEnemy
{
    //  Events ----------------------------------------
    [HideInInspector] public readonly DroneHittedUnityEvent OnDroneHitted = new DroneHittedUnityEvent();
    [HideInInspector] public readonly InitializeDroneEvent OnInitializeDroneEvent = new InitializeDroneEvent();

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
    private float droneLife;
    
    [SerializeField] 
    private float movementSpeed;
    
    [SerializeField] 
    private float shootDamage;
    
    [SerializeField] 
    private float shootSpeed;
    
    

    //  Initialization  -------------------------------
    public void Initialize(IContext context)
    {
        if (!IsInitialized)
        {
            _isInitialized = true;
            _context = context;
            
            //
            OnInitializeDroneEvent.Invoke(droneLife,movementSpeed,shootDamage,shootSpeed);
            
            //

            
            //

        }
    }
    
    public void OnTakeDamage(int damage)
    {
       OnDroneHitted.Invoke(damage);
    }


    //  Unity Methods ---------------------------------
        
        
    protected void OnDestroy()
    {
       
    }


    //  Methods ---------------------------------------


    //  Event Handlers --------------------------------

}