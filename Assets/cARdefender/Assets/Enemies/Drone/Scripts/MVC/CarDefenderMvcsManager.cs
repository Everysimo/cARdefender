using System;
using cARdefender.Assets.Enemies.Drone.Scripts.Model;
using cARdefender.Assets.Interactable.Boxes.Scripts;
using cARdefender.Assets.Interactable.Gun.Scripts.View;
using RMC.Core.Architectures.Mini.Context;

namespace cARdefender.Assets.Enemies.Drone.Scripts.MVC
{
    public class CarDefenderMvcsManager : IMiniMvcs
    {
        //  Events ----------------------------------------
        //  Properties ------------------------------------
        public bool IsInitialized
        {
            get { return _isInitialized; }
        }

        public DroneSpawnerModel DroneSpawnerModel
        {
            get { return _droneSpawnerModel; }
        }

        public DroneModel DroneModel
        {
            get { return _droneModel; }
        }

        public GunModel GunModel
        {
            get { return _gunModel; }
        }

        public PlayerModel PlayerModel
        {
            get { return _playerModel; }
        }

        public DroneView DroneView
        {
            get { return _droneView; }
        }

        public GunView GunView
        {
            get { return _gunView; }
        }

        public BoxSpawnerView BoxSpawnerView
        {
            get { return _boxSpawnerView; }
        }
        
        public BoxView BoxView
        {
            get { return _boxViewPrefab; }
        }

        public DroneController DroneController
        {
            get { return _droneController; }
        }

        public DroneService DroneService
        {
            get { return _droneService; }
        }

        //  Fields ----------------------------------------

        private bool _isInitialized = false;

        //Context
        public IContext Context { get; private set; }

        //Model
        private DroneSpawnerModel _droneSpawnerModel;
        private DroneModel _droneModel;
        private GunModel _gunModel;
        private PlayerModel _playerModel;


        //View
        private DroneSpawnerView _droneSpawnerView;
        private DroneView _droneView;
        private GunView _gunView;
        private PlayerView _playerView;
        private HandMenuView _handMenuView;
        private BoxSpawnerView _boxSpawnerView;
        private BoxView _boxViewPrefab;


        //Controller
        private DroneSpawnerController _droneSpawnerController;
        private DroneController _droneController;
        private GunController _gunController;
        private PlayerController _playerController;
        private BoxSpawnerController _boxSpawnerController;
      
        //Service
        private DroneService _droneService;


        //  Initialization  -------------------------------
        public CarDefenderMvcsManager(IContext context, DroneSpawnerView droneSpawnerView, DroneView droneView,
            GunView gunView, PlayerView playerView, HandMenuView handMenuView, BoxSpawnerView boxSpawnerView,BoxView boxViewPrefab)
        {
            _droneSpawnerView = droneSpawnerView;
            _droneView = droneView;
            _gunView = gunView;
            _playerView = playerView;
            _handMenuView = handMenuView;
            _boxSpawnerView = boxSpawnerView;
            _boxViewPrefab = boxViewPrefab;
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
                _droneSpawnerModel = new DroneSpawnerModel();
                _droneModel = new DroneModel();
                _gunModel = new GunModel();
                _playerModel = new PlayerModel();

                //Service
                _droneService = new DroneService();

                //Model
                _droneSpawnerModel.Initialize(Context);
                _droneModel.Initialize(Context);
                _gunModel.Initialize(Context);
                _playerModel.Initialize(Context);

                //View
                _droneSpawnerView.Initialize(Context);
                _droneView.Initialize(Context);
                _gunView.Initialize(Context);
                _gunView.Initialize(Context);
                _handMenuView.Initialize(Context);
                _boxSpawnerView.Initialize(Context);

                //Service
                _droneService.Initialize(Context);

                //Controller
                _droneSpawnerController =
                    new DroneSpawnerController(_droneSpawnerModel, _droneSpawnerView, _droneView);

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

                _boxSpawnerController = new BoxSpawnerController(_boxSpawnerView,_boxViewPrefab);

                //Controller (Init this main controller last)
                _droneSpawnerController.Initialize(Context);
                _droneController.Initialize(Context);
                _gunController.Initialize(Context);
                _playerController.Initialize(Context);
                _boxSpawnerController.Initialize(Context);
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