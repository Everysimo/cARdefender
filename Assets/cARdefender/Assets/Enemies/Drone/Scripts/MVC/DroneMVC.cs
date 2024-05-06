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


        
        //  Unity Methods  --------------------------------
        protected void Start()
        {
            DroneMvcsManager droneMvcs = 
                new DroneMvcsManager(_droneView,_gunView,_playerView);
            
            droneMvcs.Initialize();
        }


        //  Methods ---------------------------------------


        //  Event Handlers --------------------------------
    }
}
