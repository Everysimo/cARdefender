using cARdefender.Assets.Interactable.Gun.Scripts.View;
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

        public DroneMvcsManager droneMvcsManager;


        
        //  Unity Methods  --------------------------------
        protected void Start()
        {
            droneMvcsManager = 
                new DroneMvcsManager(_droneView,_gunView,_playerView);
            
            droneMvcsManager.Initialize();
        }


        //  Methods ---------------------------------------


        //  Event Handlers --------------------------------
    }
}
