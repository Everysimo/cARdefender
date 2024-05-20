using System;
using System.Collections;
using System.Collections.Generic;
using cARdefender.Assets.Enemies.Drone.Scripts.Model;
using cARdefender.Assets.Interactable.Gun.Scripts.Commands;
using cARdefender.Assets.Interactable.Gun.Scripts.View;
using RMC.Core.Architectures.Mini.Context;
using RMC.Core.Architectures.Mini.Controller;
using RMC.Core.Architectures.Mini.Samples.Login.WithMini.Mini.Controller.Commands;
using RMC.Core.Architectures.Mini.Samples.Login.WithMini.Mini.Model;
using UnityEngine;

//  Namespace Properties ------------------------------

//  Class Attributes ----------------------------------

/// <summary>
/// The Controller coordinates everything between
/// the <see cref="IConcern"/>s and contains the core app logic 
/// </summary>
public class DroneController : IController
{
    //  Events ----------------------------------------


    //  Properties ------------------------------------
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

    //Context
    private IContext _context;

    //  Fields ----------------------------------------

    //Model
    private DroneModel _droneModel;

    //View
    private DroneView _droneView;

    //Controller
    //private AudioController _audioController;
    


    public DroneController(DroneModel droneModel, DroneView droneView)
    {
        _droneModel = droneModel;
        
        _droneView = droneView;

    }

    //  Initialization  -------------------------------
    public void Initialize(IContext context)
    {
        if (!IsInitialized)
        {
            _isInitialized = true;
            _context = context;


            //----DRONE----
            //Model
            _droneModel.Life.OnValueChanged.AddListener(Model_Drone_OnLifeValueChanged);

            //View
            _droneView.OnInitializeDroneEvent.AddListener(View_Drone_OnInitializeDroneEvent);
            _droneView.OnDroneHitted.AddListener(View_Drone_OnDroneHitted);
            _droneView.OnDroneShootProjectile.AddListener(View_Drone_OnDroneShootProjectile);

            
            //Commands

            // Demo - Controller may update model DIRECTLY...


            // Clear
        }
    }

    public void RequireIsInitialized()
    {
        if (!_isInitialized)
        {
            throw new Exception("MustBeInitialized");
        }
    }


    //  Methods ---------------------------------------


    //  Event Handlers --------------------------------

    //-----DRONE-----

    //View
    private void View_Drone_OnInitializeDroneEvent(float droneLife, float movementSpeed, float shootDamage,
        float shootSpeed)
    {
        RequireIsInitialized();

        _droneModel.SetDroneStats(droneLife, movementSpeed, shootDamage, shootSpeed);
    }

    private void View_Drone_OnDroneHitted(float damage)
    {
        RequireIsInitialized();

        _droneModel.Life.Value -= damage;
        

        Debug.Log("Vita Drone" + _droneModel.Life.Value);
    }

    private void View_Drone_OnDroneShootProjectile(float projectileSpeed, GameObject projectilePrefab,
        Transform startPoint, Transform target)
    {
        RequireIsInitialized();

        Context.CommandManager.InvokeCommand(
            new ShootProjectileToTargetCommand(projectileSpeed, projectilePrefab, startPoint, target));
    }

    //Model
    public void Model_Drone_OnLifeValueChanged(float previousValue, float currentValue)
    {
        RequireIsInitialized();
        
        _droneView.ChangeHealthText(currentValue.ToString());
        _droneView.UpdateHealthBar(currentValue);

        if (currentValue <= 0)
        {
            Console.Write("Drone distrutto");
            Context.CommandManager.InvokeCommand(
                new DestroyDroneCommand());
            _droneView.DestroyDrone();
        }
    }
    
    
    
    
}