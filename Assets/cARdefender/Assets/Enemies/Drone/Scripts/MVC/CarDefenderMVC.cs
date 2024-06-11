using cARdefender.Assets.Interactable.Gun.Scripts.View;
using RMC.Core.Architectures.Mini.Context;
using UnityEngine;
using UnityEngine.Serialization;

namespace cARdefender.Assets.Enemies.Drone.Scripts.MVC
{
    public class CarDefenderMVC : MonoBehaviour
    {
        //  Events ----------------------------------------


        //  Properties ------------------------------------


        //  Fields ----------------------------------------

        [SerializeField] private DroneSpawnerView _droneSpawnerView;

        [SerializeField] private DroneView _droneViewPrefab;

        [SerializeField] private GunView _gunViewLeft;
        
        [SerializeField] private GunView _gunViewRight;

        [SerializeField] private PlayerView _playerView;

        [SerializeField] private HandMenuView _handMenuView;

        [SerializeField] private ShieldView _shieldView;

        [SerializeField] private CrateSpawnerView crateSpawnerView;

        [SerializeField] private HealthBoxView _healthBoxViewPrefab;

        [SerializeField] private DoubleGunBoxView _doubleGunBoxViewPrefab;

        [SerializeField] private GameManagerView _gameManagerView;

        public CarDefenderMvcsManager CarDefenderMvcsManager;


        private Context _context;


        //  Unity Methods  --------------------------------
        protected void Start()
        {
            _context = new Context();

            CarDefenderMvcsManager =
                new CarDefenderMvcsManager(_context, _droneSpawnerView, _droneViewPrefab, _gunViewRight,_gunViewLeft, _playerView,
                    _handMenuView, crateSpawnerView, _healthBoxViewPrefab, _doubleGunBoxViewPrefab, _shieldView,
                    _gameManagerView);

            CarDefenderMvcsManager.Initialize();
        }


        //  Methods ---------------------------------------


        //  Event Handlers --------------------------------
    }
}