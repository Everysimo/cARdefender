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
            _gunModel.ActualAmmo.OnValueChanged.AddListener(OnActualAmmoValueChanged);


            //View
            _gunView.OnShootButtonPressed.AddListener(View_Gun_OnShootButtonPressed);
            _gunView.OnInitializeGunEvent.AddListener(View_Gun_OnInitializeGunEvent);

            //Servie


            //Commands
            Context.CommandManager.AddCommandListener<ReloadGunAmmoCommand>(OnReloadAmmoCommand);

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

        if (_gunModel.ActualAmmo.Value <= 0 && !_gunView.isDoubleGunActive)
        {
            return;
        }

        if (!_gunView.isDoubleGunActive)
        {
            _gunModel.ActualAmmo.Value -= 1;
        }

        if (_gunView.isAutoAimActive && _gunView.AutoAimTarget!=null)
        {
            Context.CommandManager.InvokeCommand(new ShootProjectileToTargetCommand(shootSpeed/2,
                _gunModel.ShootDamage.Value, projectilePrefab, startPoint, _gunView.AutoAimTarget.transform));

            _gunView.AutoAimTarget = null;
        }
        else
        {
            Context.CommandManager.InvokeCommand(
                new ShootProjectileForwardCommand(shootSpeed, projectilePrefab, startPoint,
                    _gunModel.ShootDamage.Value));
        }
    }

    private void OnActualAmmoValueChanged(int oldValue, int newValue)
    {
        _gunView.OnAcutalAmmoChangeUI(newValue, _gunModel.MaxAmmo.Value);
    }

    private void OnReloadAmmoCommand(ReloadGunAmmoCommand reloadGunAmmoCommand)
    {
        ReloadAmmo(reloadGunAmmoCommand.amount);
    }

    private void ReloadAmmo(int amount)
    {
        _gunModel.ActualAmmo.Value += amount;
    }
}