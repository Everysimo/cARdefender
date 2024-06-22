using System;
using System.Collections;
using System.Collections.Generic;
using RMC.Core.Architectures.Mini.Context;
using RMC.Core.Architectures.Mini.Controller;
using UnityEngine;
using Random = System.Random;

public class GameFreezerController : IController
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
    Random random = new Random();

    private int _leftPassword, _rightPassword;
    
    //Model
    private GameFreezerModel _gameFreezerModel;

    //View
    private GameFreezerView _gameFreezerView;

    //Controller
    //private AudioController _audioController;



    public GameFreezerController(GameFreezerModel gameFreezerModel, GameFreezerView gameFreezerView)
    {
        _gameFreezerModel = gameFreezerModel;
        _gameFreezerView = gameFreezerView;

    }

    //  Initialization  -------------------------------
    public void Initialize(IContext context)
    {
        if (!IsInitialized)
        {
            _isInitialized = true;
            _context = context;

            //Model
            _gameFreezerModel.RightSignsMax.Value = _gameFreezerView.rightSigns.Length-1;
            _gameFreezerModel.LeftSignsMax.Value = _gameFreezerView.leftSigns.Length-1;
            
            
            //View
            _gameFreezerView.GameFreezeRequest.AddListener(ActivateGameFreeze);
            _gameFreezerView.PasswordInsert.AddListener(DeactivateGameFreeze);

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
    private void ActivateGameFreeze()
    {
        RequireIsInitialized();
        
        _rightPassword = random.Next(0,_gameFreezerModel.RightSignsMax.Value);
        _leftPassword = random.Next(0,_gameFreezerModel.LeftSignsMax.Value);

        _gameFreezerView.ActivateFreezeGame(_leftPassword, _rightPassword);
        
        Context.CommandManager.InvokeCommand(new ActivateGameFreezeCommand());

    }
    
    private void DeactivateGameFreeze()
    {
        RequireIsInitialized();

        _rightPassword = -1;
        _leftPassword = -1;
        
        Context.CommandManager.InvokeCommand(new DeactivateGameFreezeCommand());
        
    }
}