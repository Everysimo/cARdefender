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

        public GunModel GunModelRight
        {
            get { return _gunModelRight; }
        }
        
        public GunModel GunModelLeft
        {
            get { return _gunModelLeft; }
        }


        public PlayerModel PlayerModel
        {
            get { return _playerModel; }
        }

        public DroneView DroneView
        {
            get { return _droneView; }
        }
        
        public CrateSpawnerView CrateSpawnerView
        {
            get { return _crateSpawnerView; }
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
        private GunModel _gunModelRight;
        private GunModel _gunModelLeft;
        private PlayerModel _playerModel;
        private GameManagerModel _gameManagerModel;


        //View
        private DroneSpawnerView _droneSpawnerView;
        private DroneView _droneView;
        private GunView _gunViewRight, _gunViewLeft;
        private PlayerView _playerView;
        private HandMenuView _handMenuView;
        private CrateSpawnerView _crateSpawnerView;
        private HealthBoxView _healthBoxViewPrefab;
        private DoubleGunBoxView _doubleGunBoxViewPrefab;
        private ShieldView _shieldView;
        private GameManagerView _gameManagerView;


        //Controller
        private DroneSpawnerController _droneSpawnerController;
        private DroneController _droneController;
        private GunController _gunControllerRight,_gunControllerLeft;
        private PlayerController _playerController;
        private CrateSpawnerController _crateSpawnerController;
        private GameManagerController _gameManagerController;
      
        //Service
        private DroneService _droneService;


        //  Initialization  -------------------------------
        public CarDefenderMvcsManager(IContext context, DroneSpawnerView droneSpawnerView, DroneView droneView,
            GunView gunViewRight,GunView gunViewLeft, PlayerView playerView, HandMenuView handMenuView, CrateSpawnerView crateSpawnerView,
            HealthBoxView healthBoxViewPrefab,DoubleGunBoxView  doubleGunBoxViewPrefab, ShieldView shieldView,GameManagerView gameManagerView)
        {
            _droneSpawnerView = droneSpawnerView;
            _droneView = droneView;
            _gunViewRight = gunViewRight;
            _gunViewLeft = gunViewLeft;
            _playerView = playerView;
            _handMenuView = handMenuView;
            _crateSpawnerView = crateSpawnerView;
            _healthBoxViewPrefab = healthBoxViewPrefab;
            _doubleGunBoxViewPrefab = doubleGunBoxViewPrefab;
            _shieldView = shieldView;
            _gameManagerView = gameManagerView;
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
                _gunModelRight = new GunModel();
                _gunModelLeft = new GunModel();
                _playerModel = new PlayerModel();
                _gameManagerModel = new GameManagerModel();

                //Service
                _droneService = new DroneService();

                //Model
                _droneSpawnerModel.Initialize(Context);
                _droneModel.Initialize(Context);
                _gunModelRight.Initialize(Context);
                _gunModelLeft.Initialize(Context);
                _playerModel.Initialize(Context);
                _gameManagerModel.Initialize(Context);

                //View
                _droneSpawnerView.Initialize(Context);
                _droneView.Initialize(Context);
                _gunViewRight.Initialize(Context);
                _gunViewLeft.Initialize(Context);
                _handMenuView.Initialize(Context);
                _crateSpawnerView.Initialize(Context);
                _shieldView.Initialize(Context);
                _gameManagerView.Initialize(Context);
                _playerView.Initialize(Context);

                //Service
                _droneService.Initialize(Context);

                //Controller
                _droneSpawnerController =
                    new DroneSpawnerController(_droneSpawnerModel, _droneSpawnerView, _droneView);

                _droneController = new DroneController(
                    _droneModel,
                    _droneView);

                _gunControllerRight = new GunController(
                    _gunModelRight,
                    _gunViewRight);
                
                _gunControllerLeft = new GunController(
                    _gunModelLeft,
                    _gunViewLeft);

                _playerController = new PlayerController(
                    _playerModel,
                    _playerView,
                    _handMenuView);

                _crateSpawnerController = new CrateSpawnerController(_crateSpawnerView,_healthBoxViewPrefab,_doubleGunBoxViewPrefab);

                _gameManagerController = new GameManagerController(_gameManagerModel,_gameManagerView);

                //Controller (Init this main controller last)
                _droneSpawnerController.Initialize(Context);
                _droneController.Initialize(Context);
                _gunControllerRight.Initialize(Context);
                _gunControllerLeft.Initialize(Context);
                _playerController.Initialize(Context);
                _crateSpawnerController.Initialize(Context);
                _gameManagerController.Initialize(Context);
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