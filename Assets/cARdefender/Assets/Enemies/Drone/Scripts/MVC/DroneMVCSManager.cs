using System;
using cARdefender.Assets.Enemies.Drone.Scripts.Model;
using cARdefender.Assets.Interactable.Gun.Scripts.View;
using RMC.Core.Architectures.Mini.Context;

namespace cARdefender.Assets.Enemies.Drone.Scripts.MVC
{
    public class DroneMvcsManager : IMiniMvcs
    {
   
        //  Events ----------------------------------------
        //  Properties ------------------------------------
        public bool IsInitialized { get { return _isInitialized;} }
        public DroneModel DroneModel { get { return _droneModel;} }
        public GunModel GunModel { get { return _gunModel;} }
        public DroneView DroneView { get { return _droneView;} }
        public GunView GunView { get { return _gunView;} }
        public DroneController DroneController { get { return _droneController;} }
        public DroneService DroneService { get { return _droneService;} }
        
        //  Fields ----------------------------------------
 
        private bool _isInitialized = false;
        
        //Context
        public IContext Context { get; private set; }
        
        //Model
        private DroneModel _droneModel;
        private GunModel _gunModel;
        public PlayerModel _playerModel;
        
        
        //View
        private DroneView _droneView;
        private GunView _gunView;
        private PlayerView _playerView;
        private HandMenuView _handMenuView;

        
        //Controller
        private DroneController _droneController;
        private GunController _gunController;
        private PlayerController _playerController;
        
        //Service
        private DroneService _droneService;


        //  Initialization  -------------------------------
        public DroneMvcsManager(IContext context, DroneView droneView,GunView gunView, PlayerView playerView,HandMenuView handMenuView)
        {
            _droneView = droneView;
            _gunView = gunView;
            _playerView = playerView;
            _handMenuView = handMenuView;
            Context = context;
        }
        
        
        //  Initialization --------------------------------
        public void Initialize()
        {
            if (!IsInitialized)
            {
                _isInitialized = true;
                
                //Context
                Context = new Context();
                
                //Model
                _droneModel = new DroneModel();
                _gunModel = new GunModel();
                _playerModel = new PlayerModel();
   
                //Service
                _droneService = new DroneService();
            
                //Model
                _droneModel.Initialize(Context);
                _gunModel.Initialize(Context);
                _playerModel.Initialize(Context);
                
                //View
                _droneView.Initialize(Context);
                _gunView.Initialize(Context);
                _gunView.Initialize(Context);
                _handMenuView.Initialize(Context);
                
                //Service
                _droneService.Initialize(Context);
                
                //Controller
                _droneController = new DroneController(
                    _droneModel,
                    _droneView);
                
                _gunController = new GunController(
                    _gunModel,
                    _gunView);
                
                _playerController = new PlayerController(
                    _playerModel,
                    _playerView,
                    _handMenuView);
                
                //Controller (Init this main controller last)
                _droneController.Initialize(Context);
                _gunController.Initialize(Context);
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
    }
}