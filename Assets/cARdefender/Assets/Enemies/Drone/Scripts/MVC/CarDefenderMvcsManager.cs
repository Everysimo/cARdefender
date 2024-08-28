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
        private ShieldModel _shieldModelRight, _shieldModelLeft;
        private GameFreezerModel _gameFreezerModel;

        //View
        private DroneSpawnerView _droneSpawnerView;
        private DroneView _droneView;
        private GunView _gunViewRight, _gunViewLeft;
        private PlayerView _playerView;
        private HandMenuView _handMenuView;
        private CrateSpawnerView _crateSpawnerView;
        private HealthCrateView healthCrateViewPrefab;
        private DoubleGunCrateView doubleGunCrateViewPrefab;
        private AutoAimCrateView _autoAimCrateViewPrefab;
        private ShieldView _shieldViewRight, _shieldViewLeft;
        private GameManagerView _gameManagerView;
        private GameFreezerView _gameFreezerView;

        //Controller
        private DroneSpawnerController _droneSpawnerController;
        private DroneController _droneController;
        private GunController _gunControllerRight, _gunControllerLeft;
        private PlayerController _playerController;
        private CrateSpawnerController _crateSpawnerController;
        private GameManagerController _gameManagerController;
        private ShieldController _shieldControllerRight, _shieldControllerLeft;
        private GameFreezerController _gameFreezerController;

        //Service
        private DroneService _droneService;


        //  Initialization  -------------------------------
        public CarDefenderMvcsManager(IContext context, DroneSpawnerView droneSpawnerView, DroneView droneView,
            GunView gunViewRight, GunView gunViewLeft, PlayerView playerView, HandMenuView handMenuView,
            CrateSpawnerView crateSpawnerView,
            HealthCrateView healthCrateViewPrefab, DoubleGunCrateView doubleGunCrateViewPrefab,AutoAimCrateView autoAimCrateView, ShieldView shieldViewRight,
            ShieldView shieldViewLeft, GameManagerView gameManagerView, GameFreezerView gameFreezerView)
        {
            _droneSpawnerView = droneSpawnerView;
            _droneView = droneView;
            _gunViewRight = gunViewRight;
            _gunViewLeft = gunViewLeft;
            _playerView = playerView;
            _handMenuView = handMenuView;
            _crateSpawnerView = crateSpawnerView;
            this.healthCrateViewPrefab = healthCrateViewPrefab;
            this.doubleGunCrateViewPrefab = doubleGunCrateViewPrefab;
            _autoAimCrateViewPrefab = autoAimCrateView;
            _shieldViewRight = shieldViewRight;
            _shieldViewLeft = shieldViewLeft;
            _gameManagerView = gameManagerView;
            _gameFreezerView = gameFreezerView;
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
                _shieldModelRight = new ShieldModel();
                _shieldModelLeft = new ShieldModel();
                _gameFreezerModel = new GameFreezerModel();
                
                //Service
                _droneService = new DroneService();

                //Model
                _droneSpawnerModel.Initialize(Context);
                _droneModel.Initialize(Context);
                _gunModelRight.Initialize(Context);
                _gunModelLeft.Initialize(Context);
                _playerModel.Initialize(Context);
                _gameManagerModel.Initialize(Context);
                _shieldModelRight.Initialize(Context);
                _shieldModelLeft.Initialize(Context);
                _gameFreezerModel.Initialize(Context);
                
                //View
                _droneSpawnerView.Initialize(Context);
                _droneView.Initialize(Context);
                _gunViewRight.Initialize(Context);
                _gunViewLeft.Initialize(Context);
                _handMenuView.Initialize(Context);
                _crateSpawnerView.Initialize(Context);
                _shieldViewRight.Initialize(Context);
                _shieldViewLeft.Initialize(Context);
                _gameManagerView.Initialize(Context);
                _playerView.Initialize(Context);
                _gameFreezerView.Initialize(Context);
                
                healthCrateViewPrefab.Initialize(Context);
                doubleGunCrateViewPrefab.Initialize(Context);
                _autoAimCrateViewPrefab.Initialize(Context);
                
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

                _crateSpawnerController =
                    new CrateSpawnerController(_crateSpawnerView, healthCrateViewPrefab, doubleGunCrateViewPrefab,_autoAimCrateViewPrefab);

                _gameManagerController = new GameManagerController(_gameManagerModel, _gameManagerView);

                _shieldControllerRight = new ShieldController(_shieldModelRight, _shieldViewRight);

                _shieldControllerLeft = new ShieldController(_shieldModelLeft, _shieldViewLeft);

                _gameFreezerController = new GameFreezerController(_gameFreezerModel, _gameFreezerView);
                
                //Controller (Init this main controller last)
                _droneSpawnerController.Initialize(Context);
                _droneController.Initialize(Context);
                _gunControllerRight.Initialize(Context);
                _gunControllerLeft.Initialize(Context);
                _playerController.Initialize(Context);
                _crateSpawnerController.Initialize(Context);
                _gameManagerController.Initialize(Context);
                _shieldControllerRight.Initialize(Context);
                _shieldControllerLeft.Initialize(Context);
                _gameFreezerController.Initialize(Context);
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