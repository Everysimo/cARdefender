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
            _droneView.OnDronePowerUpGainedEvent.AddListener(View_Drone_OnPowerUpGained);
            _droneView.OnDronePowerUpLostEvent.AddListener(View_Drone_OnPowerUpLost);


            
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

        _droneModel.SetDroneStats(droneLife, movementSpeed, shootDamage, shootSpeed,_droneView.points);

        _droneModel.ammoReward.Value = _droneView.ammoRewards;
    }

    private void View_Drone_OnDroneHitted(float damage)
    {
        RequireIsInitialized();

        _droneModel.Life.Value -= damage;
        
    }

    private void View_Drone_OnDroneShootProjectile(float projectileSpeed, GameObject projectilePrefab,
        Transform startPoint, Transform target)
    {
        RequireIsInitialized();

        Context.CommandManager.InvokeCommand(
            new ShootProjectileToTargetCommand(projectileSpeed,_droneModel.ShootDamage.Value, projectilePrefab, startPoint, target));
    }

    private void View_Drone_OnPowerUpGained(int multyplayer)
    {
        _droneModel.ShootDamage.Value *= multyplayer;
    }
    
    private void View_Drone_OnPowerUpLost()
    {
        _droneModel.ShootDamage.Value = _droneModel.DefaultShootDamage.Value;
    }
    //Model
    public void Model_Drone_OnLifeValueChanged(float previousValue, float currentValue)
    {
        RequireIsInitialized();
        
        _droneView.ChangeHealthText(currentValue.ToString());
        _droneView.UpdateHealthBar(currentValue);

        if (currentValue <= (_droneModel.MaxLife.Value/100)*80)
        {
            _droneView.EnableDamageFX(0);
            if (currentValue <= (_droneModel.MaxLife.Value/100)*50)
            {
                _droneView.EnableDamageFX(1);
                if (currentValue <= (_droneModel.MaxLife.Value/100)*20)
                {
                    _droneView.EnableDamageFX(2);
                }
                else
                {
                    _droneView.DisableDamageFX(2);
                }
            }
            else
            {
                _droneView.DisableDamageFX(1);
                _droneView.DisableDamageFX(2);
            }
        }
        else
        {
            _droneView.DisableDamageFX(0);
            _droneView.DisableDamageFX(1);
            _droneView.DisableDamageFX(2);
        }

        if (currentValue <= 0)
        {
            Context.CommandManager.InvokeCommand(
                new DestroyDroneCommand(_droneModel.points.Value));
            
            Context.CommandManager.InvokeCommand(
                new ReloadGunAmmoCommand(_droneModel.ammoReward.Value));
            
            _droneView.DestroyDrone();
        }
    }
}