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
        
        [SerializeField] 
        private DroneSpawnerView _droneSpawnerView;
        
        [SerializeField] 
        private DroneView _droneViewPrefab;
        
        [SerializeField] 
        private GunView _gunView;
        
        [SerializeField] 
        private PlayerView _playerView;

        [SerializeField] 
        private HandMenuView _handMenuView;
        
        [FormerlySerializedAs("_boxSpawnerView")] [SerializeField] 
        private CrateSpawnerView crateSpawnerView;

        [SerializeField] 
        private BoxView _boxViewPrefab;

        public CarDefenderMvcsManager CarDefenderMvcsManager;
        
        private Context _context;


        
        //  Unity Methods  --------------------------------
        protected void Start()
        {
            _context = new Context();
            
            CarDefenderMvcsManager = 
                new CarDefenderMvcsManager(_context,_droneSpawnerView, _droneViewPrefab,_gunView,_playerView,_handMenuView,crateSpawnerView,_boxViewPrefab);
            
            CarDefenderMvcsManager.Initialize();
        }


        //  Methods ---------------------------------------


        //  Event Handlers --------------------------------
    }
}
