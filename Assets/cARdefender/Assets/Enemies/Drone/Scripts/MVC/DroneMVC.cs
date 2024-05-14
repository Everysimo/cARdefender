using cARdefender.Assets.Interactable.Gun.Scripts.View;
using RMC.Core.Architectures.Mini.Context;
using UnityEngine;

namespace cARdefender.Assets.Enemies.Drone.Scripts.MVC
{
    public class DroneMVC : MonoBehaviour
    {
        //  Events ----------------------------------------


        //  Properties ------------------------------------
        
        
        //  Fields ----------------------------------------
        
        [SerializeField] 
        private DroneView _droneView;
        
        [SerializeField] 
        private GunView _gunView;
        
        [SerializeField] 
        private PlayerView _playerView;

        [SerializeField] 
        private HandMenuView _handMenuView;

        public DroneMvcsManager droneMvcsManager;
        
        private Context _context;


        
        //  Unity Methods  --------------------------------
        protected void Start()
        {
            _context = new Context();
            
            droneMvcsManager = 
                new DroneMvcsManager(_context,_droneView,_gunView,_playerView,_handMenuView);
            
            droneMvcsManager.Initialize();
        }


        //  Methods ---------------------------------------


        //  Event Handlers --------------------------------
    }
}
