using System;
using System.Collections;
using System.Collections.Generic;
using cARdefender.Assets.Enemies.Drone.Scripts.Model;
using RMC.Core.Architectures.Mini.Context;
using RMC.Core.Architectures.Mini.Controller;
using UnityEngine;

public class GameManagerController : IController
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
    private GameManagerModel _gameManagerModel;

    //View
    private GameManagerView _gameManagerView;

    //Controller
    //private AudioController _audioController;
    


    public GameManagerController(GameManagerModel gameManagerModel, GameManagerView gameManagerView)
    {
        _gameManagerModel = gameManagerModel;
        _gameManagerView = gameManagerView;

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
            _gameManagerModel.score.OnValueChanged.AddListener(OnScoreValueChanged);
            
            _gameManagerModel.livesLost.OnValueChanged.AddListener(OnLivesLostValueChanged);
            //View
            
            
            //Commands
            Context.CommandManager.AddCommandListener<DestroyDroneCommand>(
                OnDroneDestroyed);
            
            Context.CommandManager.AddCommandListener<PlayerLifeLostCommand>(
                OnPlayerLifeLost);
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
    public void OnDroneDestroyed(DestroyDroneCommand destroyDroneCommand)
    {
        _gameManagerModel.score.Value += destroyDroneCommand.dronePoints;
    }
    public void OnPlayerLifeLost(PlayerLifeLostCommand playerLifeLostCommand)
    {
        _gameManagerModel.livesLost.Value += 1;
        
        _gameManagerModel.score.Value /= 2;
    }
    

    //  Event Handlers --------------------------------

    public void OnScoreValueChanged(int oldValue, int newValue)
    {
        _gameManagerView.UpdateScoreView(newValue);
    }
    
    public void OnLivesLostValueChanged(int oldValue, int newValue)
    {
        _gameManagerView.UpdateLivesLostView(newValue);
    }

    //-----DRONE-----

    //View


    //Model
    
}
