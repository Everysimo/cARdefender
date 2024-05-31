using cARdefender.Assets.Interactable.Gun.Scripts.View;
using RMC.Core.Architectures.Mini.Context;
using UnityEngine;

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
        
        [SerializeField] 
        private BoxSpawnerView _boxSpawnerView;

        [SerializeField] 
        private BoxView _boxViewPrefab;

        public CarDefenderMvcsManager CarDefenderMvcsManager;
        
        private Context _context;


        
        //  Unity Methods  --------------------------------
        protected void Start()
        {
            _context = new Context();
            
            CarDefenderMvcsManager = 
                new CarDefenderMvcsManager(_context,_droneSpawnerView, _droneViewPrefab,_gunView,_playerView,_handMenuView,_boxSpawnerView,_boxViewPrefab);
            
            CarDefenderMvcsManager.Initialize();
        }


        //  Methods ---------------------------------------


        //  Event Handlers --------------------------------
    }
}
