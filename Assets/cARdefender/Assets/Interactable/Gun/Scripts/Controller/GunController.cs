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
public class GunController : IController
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

    //View
    private GunView _gunView;

    //Controller
    //private AudioController _audioController;

    //Service
    DroneService _droneService;


    public GunController(GunModel gunModel, GunView gunView)
    {
        _gunModel = gunModel;
        
        _gunView = gunView;

    }

    //  Initialization  -------------------------------
    public void Initialize(IContext context)
    {
        if (!IsInitialized)
        {
            _isInitialized = true;
            _context = context;
            
            //----GUN----
            //Model

            //View
            _gunView.OnShootButtonPressed.AddListener(View_Gun_OnShootButtonPressed);
            _gunView.OnInitializeGunEvent.AddListener(View_Gun_OnInitializeGunEvent);

            //Servie
            


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
            new ShootProjectileForwardCommand(shootSpeed, projectilePrefab, startPoint, _gunModel.ShootDamage.Value));
    }
    
    //-----HAND MENU-----
    
    //View 
    
    
    
}