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
public class PlayerController : IController
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
    public PlayerModel _playerModel;

    //View
    private PlayerView _playerView;
    private HandMenuView _handMenuView;

    //Controller
    //private AudioController _audioController;

    //Service
    DroneService _droneService;


    public PlayerController(PlayerModel playerModel, PlayerView playerView,HandMenuView handMenuView)
    {
        _playerModel = playerModel;
        
        _playerView = playerView;
        _handMenuView = handMenuView;

    }

    //  Initialization  -------------------------------
    public void Initialize(IContext context)
    {
        if (!IsInitialized)
        {
            _isInitialized = true;
            _context = context;
            
            
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
    
    
    //----PLAYER-----
    
    //View
    private void View_Player_OnPlayerHitted(float damage)
    {
        RequireIsInitialized();
        

        _playerModel.Life.Value = _playerModel.Life.Value - damage;
        
        _handMenuView.UpdatePlayerLifeUI(_playerModel.Life.Value);
    }
    
    //-----HAND MENU-----
    
    //View 
    
    
    
}