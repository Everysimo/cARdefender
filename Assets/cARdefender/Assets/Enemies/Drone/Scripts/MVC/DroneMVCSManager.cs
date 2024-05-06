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
        public Context Context { get { return _context;} }
        public DroneModel DroneModel { get { return _droneModel;} }
        public GunModel GunModel { get { return _gunModel;} }
        public DroneView DroneView { get { return _droneView;} }
        public GunView GunView { get { return _gunView;} }
        public DroneController DroneController { get { return _droneController;} }
        public DroneService DroneService { get { return _droneService;} }
        
        //  Fields ----------------------------------------
 
        private bool _isInitialized = false;
        
        //Context
        private Context _context;
        
        //Model
        private DroneModel _droneModel;
        private GunModel _gunModel;
        public PlayerModel _playerModel;
        
        
        //View
        private DroneView _droneView;
        private GunView _gunView;
        private PlayerView _playerView;

        
        //Controller
        private DroneController _droneController;
        
        //Service
        private DroneService _droneService;


        //  Initialization  -------------------------------
        public DroneMvcsManager(DroneView droneView,GunView gunView, PlayerView playerView)
        {
            _droneView = droneView;
            _gunView = gunView;
            _playerView = playerView;
        }
        
        
        //  Initialization --------------------------------
        public void Initialize()
        {
            if (!IsInitialized)
            {
                _isInitialized = true;
                
                //Context
                _context = new Context();
                
                //Model
                _droneModel = new DroneModel();
                _gunModel = new GunModel();
                _playerModel = new PlayerModel();
   
                //Service
                _droneService = new DroneService();
                
                //Controller
                _droneController = new DroneController(
                    _droneModel,
                    _gunModel,
                    _playerModel,
                    _gunView,
                    _droneView,
                    _playerView,
                    _droneService);
            
                //Model
                _droneModel.Initialize(_context);
                _gunModel.Initialize(_context);
                _playerModel.Initialize(_context);
                
                //View
                _droneView.Initialize(_context);
                _gunView.Initialize(_context);
                _gunView.Initialize(_context);
                
                //Service
                _droneService.Initialize(_context);
                
              
                
                //Controller (Init this main controller last)
                _droneController.Initialize(_context);
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