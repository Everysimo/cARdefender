using System;
using System.Collections;
using RMC.Core.Architectures.Mini.Context;
using RMC.Core.Architectures.Mini.View;
using TMPro;
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
        public int maxAmmo;
    
        [SerializeField] 
        public int actualAmmo;
    
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

        [SerializeField] private TextMeshProUGUI ammoUI;
        
        [SerializeField] 
        private AudioClip _shootSound;
        [SerializeField] 
        private AudioClip _gunOnSound;
        [SerializeField] 
        private AudioClip _gunOffSound;
        [SerializeField] 
        private AudioClip _gunEmptySound;
        
        [SerializeField] 
        private AudioSource _audioSource;

        [SerializeField] private GameObject[] visuals;
        
        public bool canShoot = false;
        public bool isDoubleGunActive = false;
        public bool isPrimary = false;
        public bool isAutoAimActive = false;

        
        public float shootSpeed = 1;
        
        public float PowerUpShootSpeed = 1;

        public GameObject AutoAimTarget;

        private Coroutine _shootingCoroutine;
        
        private bool _isGameFrozen;


        //  Initialization  -------------------------------
        public void Initialize(IContext context)
        {
            if (!IsInitialized)
            {
                _isInitialized = true;
            
                _context = context;

                canShoot = false;
                isDoubleGunActive = false;
                isAutoAimActive = false;
                
                Context.CommandManager.AddCommandListener<ActiveDoubleGunCommand>(ActiveDoubleGunPowerUp);
                
                Context.CommandManager.AddCommandListener<ActivateGameFreezeCommand>(ActivateGameFreezeStatus);
                Context.CommandManager.AddCommandListener<DeactivateGameFreezeCommand>(DeactivateGameFreezeStatus);

                
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
            if (_isGameFrozen)
            {
                return;
            }
            if (actualAmmo <= 0)
            {
                _audioSource.PlayOneShot(_gunEmptySound);
                return;
            }
            if (canShoot || isDoubleGunActive)
            {
                if (!isDoubleGunActive && !isPrimary)
                {
                    return;
                }

                if (isAutoAimActive)
                {
                    GameObject[] objs = FindObjectsOfType<GameObject>();

                    foreach (GameObject obj in objs)
                    {
                        // Controlla se l'oggetto ha lo script DroneView
                        DroneView targetObject = obj.GetComponent<DroneView>();
                        if (targetObject != null)
                        {
                            // Ottieni il Transform dell'oggetto trovato
                            AutoAimTarget = targetObject.gameObject;
                            break; // Esci dal loop una volta trovato l'oggetto desiderato
                        }
                        else
                        {
                            AutoAimTarget = null;
                        }
                    }
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
            if (_shootingCoroutine != null)
            {
                StopCoroutine(_shootingCoroutine);
                _shootingCoroutine = null;
            }
            isDoubleGunActive = true;
            _shootingCoroutine = StartCoroutine(ShootEveryXSeconds(activeDoubleGunCommand._duration));
        }

        public IEnumerator ShootEveryXSeconds(float powerUpDuration)
        {
            float elapsedTime = 0f;
            while (true)
            {
                ShootButtonPressed();

                float waitTime = isDoubleGunActive ? PowerUpShootSpeed : shootSpeed;
                yield return new WaitForSeconds(waitTime);

                if (isDoubleGunActive)
                {
                    elapsedTime += waitTime;
                    if (elapsedTime >= powerUpDuration)
                    {
                        isDoubleGunActive = false;
                        DeactivateDoubleGunPowerUp();
                    }
                }
            }
        }

        public void DeactivateDoubleGunPowerUp()
        {
            if (_shootingCoroutine != null)
            {
                StopCoroutine(_shootingCoroutine);
                _shootingCoroutine = null;
            }
            isDoubleGunActive = false;
            DeactivateGun();
        }

        //  Unity Methods ---------------------------------

        //  Methods ---------------------------------------

        public void ActivateGun()
        {
            if (!isDoubleGunActive && !isPrimary || _shootingCoroutine != null)
            {
                return;
            }
            _audioSource.PlayOneShot(_gunOnSound);
            ToggleCanShootTrue();
            foreach (GameObject visual in visuals)
            {
                visual.gameObject.SetActive(true);
            }
            if (_shootingCoroutine == null)
            {
                _shootingCoroutine = StartCoroutine(ShootEveryXSeconds(float.MaxValue));
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
            if (_shootingCoroutine != null)
            {
                StopCoroutine(_shootingCoroutine);
                _shootingCoroutine = null;
            }
        }

        public void ToggleCanShootTrue()
        {
            canShoot = true;
        }
        
        public void ToggleCanShootFalse()
        {
            canShoot = false;
        }
        
        public void ActivateGameFreezeStatus(ActivateGameFreezeCommand activateGameFreezeCommand)
        {
            _isGameFrozen = true;
            DeactivateGun();
        }
    
        public void DeactivateGameFreezeStatus(DeactivateGameFreezeCommand deactivateGameFreezeCommand)
        {
            _isGameFrozen = false;
        }

        public void OnAcutalAmmoChangeUI(int newValue,int newMaxAmmo)
        {
            actualAmmo = newValue;
            maxAmmo = newMaxAmmo;
            
            ammoUI.text = ""+actualAmmo;
        }

        //  Event Handlers --------------------------------
        

    }
}