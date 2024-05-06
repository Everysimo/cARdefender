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
    private GunModel _gunModel;
    private DroneModel _droneModel;
    public PlayerModel _playerModel;

    //View
    private GunView _gunView;
    private DroneView _droneView;
    private PlayerView _playerView;

    //Controller
    //private AudioController _audioController;

    //Service
    DroneService _droneService;


    public DroneController(DroneModel droneModel, GunModel gunModel, PlayerModel playerModel, GunView gunView,
        DroneView droneView,PlayerView playerView, DroneService droneService)
    {
        _gunModel = gunModel;
        _droneModel = droneModel;
        _playerModel = playerModel;
        
        _gunView = gunView;
        _droneView = droneView;
        _playerView = playerView;
        
        _droneService = droneService;
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

            //Servie
            //_service.OnSumCalculatedEvent.AddListener(Service_OnSumCalculated);

            //----GUN----
            //Model

            //View
            _gunView.OnShootButtonPressed.AddListener(View_Gun_OnShootButtonPressed);
            _gunView.OnInitializeGunEvent.AddListener(View_Gun_OnInitializeGunEvent);

            //Servie
            
            //----PLAYER----
            //Model

            //View
            _playerView.OnPlayerHitted.AddListener(View_Player_OnPlayerHitted);



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

        if (currentValue <= 0)
        {
            Console.Write("Drone distrutto");
            _droneView.DestroyDrone();
        }
    }

    //-----GUN-----

    //View
    private void View_Gun_OnInitializeGunEvent(int maxAmmo, float reloadSpeed, float shootDamage, float shootSpeed)
    {
        RequireIsInitialized();

        _gunModel.SetGunStats(maxAmmo, maxAmmo, reloadSpeed, shootDamage, shootSpeed);
    }

    private void View_Gun_OnShootButtonPressed(float shootSpeed, GameObject projectilePrefab, Transform startPoint)
    {
        RequireIsInitialized();


        Context.CommandManager.InvokeCommand(
            new ShootProjectileWithGravity(shootSpeed, projectilePrefab, startPoint, _gunModel.ShootDamage.Value));
    }
    
    //----PLAYER-----
    
    //View
    private void View_Player_OnPlayerHitted(float damage)
    {
        RequireIsInitialized();
        
        Debug.Log("Drone damage " + damage);

        _playerModel.Life.Value = _playerModel.Life.Value - damage;
        
        Debug.Log("Vita Giocatore " + _playerModel.Life.Value);
        
    }
}