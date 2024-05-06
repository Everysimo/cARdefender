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

public class DroneShootProjectileUnityEvent : UnityEvent<float,GameObject,Transform,Transform>{}
public class InitializeDroneEvent : UnityEvent<float,float,float,float>{}


/// <summary>
/// The View handles user interface and user input
/// </summary>
public class DroneView : MonoBehaviour, IView, IHittableEnemy
{
    //  Events ----------------------------------------
    [HideInInspector] public readonly DroneHittedUnityEvent OnDroneHitted = new DroneHittedUnityEvent();
    [HideInInspector] public readonly DroneShootProjectileUnityEvent OnDroneShootProjectile = new DroneShootProjectileUnityEvent();
    [HideInInspector] public readonly InitializeDroneEvent OnInitializeDroneEvent = new InitializeDroneEvent();
    
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
    private float droneLife;
    
    [SerializeField] 
    private float movementSpeed;
    
    [SerializeField] 
    private float shootDamage;
    
    [SerializeField] 
    private float shootSpeed;
    
    [SerializeField] 
    private float projectileSpeed;
    
    [SerializeField]
    [Tooltip("The projectile that's created")]
    GameObject projectilePrefab = null;

    [SerializeField]
    [Tooltip("The point that the project is created")]
    Transform[] startPoints = null;
    
    [SerializeField]
    [Tooltip("The target object to aim to")]
    Transform targetObject = null;

    private int lastStartPointWeaponNumber = 0;

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
        OnInitializeDroneEvent.Invoke(droneLife,movementSpeed,shootDamage,shootSpeed);
    }

    public void OnTakeDamage(float damage)
    {
       OnDroneHitted.Invoke(damage);
    }

    public IEnumerator OnShootProjectile()
    {
        while (true)
        {
            yield return new WaitForSeconds(shootSpeed);
            
            OnDroneShootProjectile.Invoke(projectileSpeed,projectilePrefab,startPoints[lastStartPointWeaponNumber],targetObject);
            lastStartPointWeaponNumber = (lastStartPointWeaponNumber + 1 )% (startPoints.Length);
            
            //audioSource.PlayOneShot(spitSound);
        }
    }


    //  Unity Methods ---------------------------------

    private void OnEnable()
    {
        _coroutine = StartCoroutine(OnShootProjectile());
    }

    private void OnDisable()
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
            _coroutine = null;
        }
    }
    

    //  Methods ---------------------------------------

    public void DestroyDrone()
    {
        Destroy(gameObject);
    }

    //  Event Handlers --------------------------------
    
}