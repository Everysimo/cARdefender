using System;
using RMC.Core.Architectures.Mini.Context;
using RMC.Core.Architectures.Mini.View;
using UnityEngine;
using UnityEngine.Events;

namespace cARdefender.Assets.Interactable.Gun.Scripts.View
{
    //  Namespace Properties ------------------------------

//  Class Attributes ----------------------------------
    public class ShootButtonPressedUnityEvent : UnityEvent<float,GameObject,Transform>{}
    public class ReloadButtonPressedUnityEvent : UnityEvent{}
    public class InitializeGunEvent : UnityEvent<int,float,float,float>{}


    /// <summary>
    /// The View handles user interface and user input
    /// </summary>
    public class GunView : MonoBehaviour, IView
    {
        //  Events ----------------------------------------
        [HideInInspector] public readonly ShootButtonPressedUnityEvent OnShootButtonPressed = new ShootButtonPressedUnityEvent();
        [HideInInspector] public readonly ReloadButtonPressedUnityEvent OnReloadButtonPressed = new ReloadButtonPressedUnityEvent();
        [HideInInspector] public readonly InitializeGunEvent OnInitializeGunEvent = new InitializeGunEvent();

        //  Properties ------------------------------------
        public void RequireIsInitialized()
        {
            if (!IsInitialized)
            {
                throw new Exception("MustBeInitialized");
            }
        }

        public bool IsInitialized
        {
            get { return _isInitialized; }
        }

        public IContext Context
        {
            get { return _context; }
        }


        //  Fields ----------------------------------------
        private bool _isInitialized = false;
        private IContext _context;
    
        [SerializeField] 
        private int maxAmmo;
    
        [SerializeField] 
        private int actualAmmo;
    
        [SerializeField] 
        private float reloadSpeed;
    
        [SerializeField] 
        private float shootDamage;
    
        [SerializeField] 
        private float shootSpeed;
    
        [SerializeField]
        [Tooltip("The projectile that's created")]
        GameObject projectilePrefab = null;

        [SerializeField]
        [Tooltip("The point that the project is created")]
        Transform startPoint = null;
    
    

        //  Initialization  -------------------------------
        public void Initialize(IContext context)
        {
            if (!IsInitialized)
            {
                _isInitialized = true;
            
                _context = context;
                
            
                //

            
                //

            }
        }

        private void Start()
        {
            OnInitializeGunEvent.Invoke(maxAmmo,reloadSpeed,shootDamage,shootSpeed);
        }

        public void InitalizeGunEvent()
        {
            OnInitializeGunEvent.Invoke(maxAmmo,reloadSpeed,shootDamage,shootSpeed);
        }

        public void ShootButtonPressed()
        {
            OnShootButtonPressed.Invoke(shootSpeed,projectilePrefab,startPoint);
        }
    
        public void ReloadButtonPressed()
        {
            OnReloadButtonPressed.Invoke();
        }


        //  Unity Methods ---------------------------------
        
        
        protected void OnDestroy()
        {
       
        }


        //  Methods ---------------------------------------


        //  Event Handlers --------------------------------
        

    }
}