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
                _boxView.OnBoxHittedEvent.AddListener(View_OnLifeBoxHitted);

            
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

        public void View_OnLifeBoxHitted(float lifeToRecover)
        {
            Context.CommandManager.InvokeCommand(
                new PlayerRecoverLifeCommand(lifeToRecover));

            PlayerModel playerModel = Context.ModelLocator.GetItem<PlayerModel>();
            playerModel.Life.Value += lifeToRecover;
        
            _boxView.DisableBox();
        }

        //  Event Handlers --------------------------------

        //-----DRONE-----

        //View

        //Model
    
    
    
    
    
    }
}
