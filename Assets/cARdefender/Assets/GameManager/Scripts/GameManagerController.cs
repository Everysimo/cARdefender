using System;
using System.Collections;
using System.Collections.Generic;
using cARdefender.Assets.Enemies.Drone.Scripts.Model;
using RMC.Core.Architectures.Mini.Context;
using RMC.Core.Architectures.Mini.Controller;
using RMC.Core.Architectures.Mini.Samples.SpawnerMini.WithMini.Mini.Controller.Commands;
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
            
            _gameManagerModel.gameLevel.OnValueChanged.AddListener(OnGameLevelValueChanged);
            //View
            
            _gameManagerView.OnGameStartEvent.AddListener(OnGameStart);
            
            //Commands
            Context.CommandManager.AddCommandListener<DestroyDroneCommand>(
                OnDroneDestroyed);
            
            Context.CommandManager.AddCommandListener<PlayerLifeLostCommand>(
                OnPlayerLifeLost);
            
            Context.CommandManager.AddCommandListener<LevelChangeRequestCommand>(OnGameLevelChangeRequest);
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

    public void OnGameStart()
    {
        _gameManagerModel.gameLevel.Value = 1;
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

    public void OnGameLevelChangeRequest(LevelChangeRequestCommand levelChangeRequestCommand)
    {
        _gameManagerModel.gameLevel.Value += 1;
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

    public void OnGameLevelValueChanged(int oldValue, int newValue)
    {
        int dronesToSpawn = newValue;
        if (dronesToSpawn < 3)
        {
            dronesToSpawn = 1;
        }
        else
        {
            dronesToSpawn /= 3;
        }
        if (dronesToSpawn > 7)
        {
            dronesToSpawn = 7;
        }
        Context.CommandManager.InvokeCommand(new GameLevelChangedCommand(dronesToSpawn));
        _gameManagerView.UpdateLevelView(_gameManagerModel.gameLevel.Value);
    }
    
    //-----DRONE-----

    //View


    //Model
    
}
