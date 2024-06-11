using System;
using System.Collections;
using RMC.Core.Architectures.Mini.Context;
using RMC.Core.Architectures.Mini.View;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

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
        private float ammoSpeed;
    
        [SerializeField]
        [Tooltip("The projectile that's created")]
        GameObject projectilePrefab = null;

        [SerializeField]
        [Tooltip("The point that the project is created")]
        Transform startPoint = null;
        
        [SerializeField] 
        private AudioClip _shootSound;
        [SerializeField] 
        private AudioClip _gunOnSound;
        [SerializeField] 
        private AudioClip _gunOffSound;
        
        [SerializeField] 
        private AudioSource _audioSource;

        [SerializeField] private GameObject[] visuals;
        
        public bool canShoot = false;
        public bool isDoubleGunActive = false;
        public bool isPrimary = false;
        
        // Start is called before the first frame update
        public float shootSpeed = 1;
        
        public float PowerUpShootSpeed = 1;

        public IEnumerator ShootEveryXSeconds(float seconds)
        {
            while (true)
            {
                ShootButtonPressed();
                yield return new WaitForSeconds(seconds); 
            }
        }

        private Coroutine _basicShootingCoroutine, _powerUpShootingCoroutine;


        //  Initialization  -------------------------------
        public void Initialize(IContext context)
        {
            if (!IsInitialized)
            {
                _isInitialized = true;
            
                _context = context;

                canShoot = false;
                isDoubleGunActive = false;
                
                Context.CommandManager.AddCommandListener<ActiveDoubleGunCommand>(ActiveDoubleGunPowerUp);

                //


                //

            }
        }

        private void Start()
        {
            OnInitializeGunEvent.Invoke(maxAmmo,reloadSpeed,shootDamage,ammoSpeed);
        }

        public void InitalizeGunEvent()
        {
            OnInitializeGunEvent.Invoke(maxAmmo,reloadSpeed,shootDamage,ammoSpeed);
        }

        public void ShootButtonPressed()
        {
            if (canShoot || isDoubleGunActive)
            {
                if (!isDoubleGunActive && !isPrimary)
                {
                    return;
                }
                _audioSource.PlayOneShot(_shootSound);
                OnShootButtonPressed.Invoke(ammoSpeed,projectilePrefab,startPoint);
            }
        }
    
        public void ReloadButtonPressed()
        {
            OnReloadButtonPressed.Invoke();
        }

        public void ActiveDoubleGunPowerUp(ActiveDoubleGunCommand activeDoubleGunCommand)
        {
            isDoubleGunActive = true;
            if (_basicShootingCoroutine != null)
            {
                 StopCoroutine(_basicShootingCoroutine);
                 _basicShootingCoroutine = null;
            }
            if (_powerUpShootingCoroutine != null)
            {
                StopCoroutine(_powerUpShootingCoroutine);
                _powerUpShootingCoroutine = null;
            }
            StopAllCoroutines();
            _powerUpShootingCoroutine = StartCoroutine(ActivateDoubleGunPowerUp(activeDoubleGunCommand._duration));
        }
        
        public IEnumerator ActivateDoubleGunPowerUp(float seconds)
        {
            ActivateGun();
            yield return new WaitForSeconds(seconds); 
            DeactivateDoubleGunPowerUp();
        }

        public void DeactivateDoubleGunPowerUp()
        {
            if (_powerUpShootingCoroutine != null)
            {
                StopCoroutine(_powerUpShootingCoroutine);
                _powerUpShootingCoroutine = null;
            }
            isDoubleGunActive = false;
            DeactivateGun();
        }


        //  Unity Methods ---------------------------------
        


        //  Methods ---------------------------------------

        public void ActivateGun()
        {
            if (!isDoubleGunActive && !isPrimary || _powerUpShootingCoroutine != null || _basicShootingCoroutine != null)
            {
                return;
            }
            _audioSource.PlayOneShot(_gunOnSound);
            ToggleCanShootTrue();
            foreach (GameObject visual in visuals)
            {
                visual.gameObject.SetActive(true);
            }
            if (isDoubleGunActive && _powerUpShootingCoroutine == null)
            {
                _powerUpShootingCoroutine = StartCoroutine(ShootEveryXSeconds(PowerUpShootSpeed));
            }
            if(_basicShootingCoroutine == null)
            {
                _basicShootingCoroutine = StartCoroutine(ShootEveryXSeconds(shootSpeed));
            }
            
        }
        
        public void DeactivateGun()
        {
            if (isDoubleGunActive)
            {
                return;
            }
            if (!isDoubleGunActive && !isPrimary)
            {
                return;
            }
            _audioSource.PlayOneShot(_gunOffSound);
            ToggleCanShootFalse();
            
            foreach (GameObject visual in visuals)
            {
                visual.gameObject.SetActive(false);
            }
            if (_powerUpShootingCoroutine != null)
            {
                StopCoroutine(_powerUpShootingCoroutine);
                _powerUpShootingCoroutine = null;
            }
            if (_basicShootingCoroutine != null)
            {
                StopCoroutine(_basicShootingCoroutine);
                _basicShootingCoroutine = null;
            }
            StopAllCoroutines();
        }

        public void ToggleCanShootTrue()
        {
            canShoot = true;
        }
        
        public void ToggleCanShootFalse()
        {
            canShoot = false;
        }
        //  Event Handlers --------------------------------
        

    }
}