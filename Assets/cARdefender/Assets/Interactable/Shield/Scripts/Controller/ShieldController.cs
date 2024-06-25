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
using UnityEngine.Events;

//  Namespace Properties ------------------------------

//  Class Attributes ----------------------------------

/// <summary>
/// The Controller coordinates everything between
/// the <see cref="IConcern"/>s and contains the core app logic 
/// </summary>
/// 

public class ShieldController : IController
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
    private ShieldModel _shieldModel;

    //View
    private ShieldView _shieldView;

    //Controller
    //private AudioController _audioController;
    


    public ShieldController(ShieldModel shieldModel, ShieldView shieldView)
    {
        _shieldModel = shieldModel;
        _shieldView = shieldView;

    }

    //  Initialization  -------------------------------
    public void Initialize(IContext context)
    {
        if (!IsInitialized)
        {
            _isInitialized = true;
            _context = context;

            _shieldModel.MaxCounterTime.Value = _shieldView.MaxActiveTime;
            _shieldModel.ActualCounterTime.Value = _shieldView.MaxActiveTime;
            
            //----GUN----
            //Model
            _shieldModel.ActualCounterTime.OnValueChanged.AddListener(OnActualCounterValueChanged);
            _shieldModel.IsInBurnout.OnValueChanged.AddListener(OnBurnoutValueChanged);

            //View
            _shieldView.OnActualCounterValueUpdateEvent.AddListener(View_Shield_OnUpdateActualCounter);
            _shieldView.OnShieldHittedEvent.AddListener(OnShieldHitted);
            
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
    

    //View
    private void View_Shield_OnUpdateActualCounter(int timeToAdd)
    {
        RequireIsInitialized();

        int newTime = _shieldModel.ActualCounterTime.Value + timeToAdd;

        if (newTime <= 0)
        {
            _shieldModel.IsInBurnout.Value = true;
            _shieldView.ToggleBurnoutMode();
            newTime = 0;
        }
        if (newTime >= _shieldModel.MaxCounterTime.Value)
        {
            _shieldView.ShieldIsReloaded();
            newTime = _shieldModel.MaxCounterTime.Value;
        }

        _shieldModel.ActualCounterTime.Value = newTime;

    }

    private void OnActualCounterValueChanged(int oldValue,int newValue)
    {
        if (_shieldModel.IsInBurnout.Value)
        {
            if (_shieldModel.ActualCounterTime.Value == _shieldModel.MaxCounterTime.Value)
            {
                _shieldModel.IsInBurnout.Value = false;
            }
        }
        _shieldView.UpdateTextLabel(newValue);
    }

    private void OnBurnoutValueChanged(bool oldValue, bool newValue)
    {
        _shieldView.isInBurnout = newValue;
    }

    private void OnShieldHitted()
    {
        Debug.Log("Shield Hitted Controller");
        Context.CommandManager.InvokeCommand(
            new ReloadGunAmmoCommand(5));
    }
    
    
    
    //-----HAND MENU-----
    
    //View 
    
    
    
}