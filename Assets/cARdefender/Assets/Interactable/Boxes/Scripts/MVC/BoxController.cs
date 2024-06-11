using System;
using cARdefender.Assets.Enemies.Drone.Scripts.Model;
using RMC.Core.Architectures.Mini.Context;
using UnityEngine;
using IController = RMC.Core.Architectures.Mini.Controller.IController;

namespace cARdefender.Assets.Interactable.Boxes.Scripts
{
    public class BoxController : IController
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
        private BoxModel _boxModel;

        //View
        private BoxView _boxView;

        //Controller
        //private AudioController _audioController;
    


        public BoxController(BoxModel boxModel, BoxView boxView)
        {
            _boxModel = boxModel;
            _boxView = boxView;

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


                //View
                _boxView.OnHealthBoxHittedEvent.AddListener(HealthBoxView_OnLifeBoxHitted);
                _boxView.OnDoubleGunBoxHittedEvent.AddListener(DoubleGunBoxView_OnBoxHitted);
            
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

        public void HealthBoxView_OnLifeBoxHitted(float lifeToRecover)
        {
            Context.CommandManager.InvokeCommand(
                new PlayerRecoverLifeCommand(lifeToRecover));
        
            _boxView.DisableBox();
        }

        public void DoubleGunBoxView_OnBoxHitted(int duration)
        {
            Context.CommandManager.InvokeCommand(new ActiveDoubleGunCommand(duration));
            _boxView.DisableBox();
        }
        //  Event Handlers --------------------------------

        //-----DRONE-----

        //View

        //Model
    
    
    
    
    
    }
}
