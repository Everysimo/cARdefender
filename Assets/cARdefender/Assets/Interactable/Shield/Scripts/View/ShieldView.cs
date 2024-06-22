using System;
using System.Collections;
using System.Collections.Generic;
using cARdefender.Assets.Enemies.Generic_Enemies.Scripts;
using RMC.Core.Architectures.Mini.Context;
using RMC.Core.Architectures.Mini.View;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class ShieldHittedUnityEvent : UnityEvent{}
public class UpdateActualCounterValyeUnityEvent : UnityEvent<int>{}

public class ShieldView : MonoBehaviour,IView,IHittableObject
{
    private Coroutine _ReloadCounter,_DownloadCounter;
     
    [HideInInspector] public readonly ShieldHittedUnityEvent OnShieldHittedEvent = new ShieldHittedUnityEvent();
    [HideInInspector] public readonly UpdateActualCounterValyeUnityEvent OnActualCounterValueUpdateEvent = new UpdateActualCounterValyeUnityEvent();
    
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

    IContext IInitializableWithContext.Context => Context;

    public IContext Context
    {
        get { return _context; }
    }

    //  Fields ----------------------------------------
    private bool _isInitialized = false;
    private IContext _context;

    public int MaxActiveTime;
    
    public bool isInBurnout;
    public bool isActive;

    private bool _isGameFrozen;

    [SerializeField] private GameObject[] visuals;
    
    [SerializeField] private TextMeshProUGUI counterLabel;
    [SerializeField] private AudioClip TurnOnSound;
    [SerializeField] private AudioClip TurnOffSound;
    [SerializeField] private AudioClip BurnOutOnSound;
    [SerializeField] private AudioClip BurnOutOffSound;

    public GameObject ShieldOkIcon;
    public GameObject ShieldBrokenIcon;
    
    [SerializeField] private AudioSource _audioSource;

    //  Initialization  -------------------------------
    public void Initialize(IContext context)
    {
        if (!IsInitialized)
        {
            _isInitialized = true;
            _context = context;
            
            //
            
            Context.CommandManager.AddCommandListener<ActivateGameFreezeCommand>(ActivateGameFreezeStatus);
            Context.CommandManager.AddCommandListener<DeactivateGameFreezeCommand>(DeactivateGameFreezeStatus);


            //

        }
    }

    //  Unity Methods ---------------------------------
    public void OnShieldActivation()
    {
        if (_isGameFrozen)
        {
            return;
        }
        if (isInBurnout)
        {
            return;
        }
        if (TurnOnSound)
        {
            _audioSource.PlayOneShot(TurnOnSound);
        }
        isActive = true;
        foreach (GameObject visual in visuals)
        {
            visual.gameObject.SetActive(true);
        }
        StopAllCoroutines();
        _DownloadCounter = StartCoroutine(DownloadCounterCorutine());

    }
    
    public void OnBurnoutShieldDeactivation()
    {
        isActive = false;
        if (TurnOffSound)
        {
            _audioSource.PlayOneShot(TurnOffSound);
        }
           
        foreach (GameObject visual in visuals)
        {
            visual.gameObject.SetActive(false);
        }
        StopAllCoroutines();
        _ReloadCounter = StartCoroutine(ReloadCounterCorutine());
    }
    
    public void OnShieldDeactivation()
    {
        if (_isGameFrozen)
        {
            return;
        }
        if (isInBurnout)
        {
            return;
        }
        isActive = false;
        if (TurnOffSound)
        {
            _audioSource.PlayOneShot(TurnOffSound);
        }
           
        foreach (GameObject visual in visuals)
        {
            visual.gameObject.SetActive(false);
        }
        StopAllCoroutines();
        _ReloadCounter = StartCoroutine(ReloadCounterCorutine());
    }

    public IEnumerator DownloadCounterCorutine()
    {
        while (isActive && !isInBurnout)
        {
            yield return new WaitForSeconds(1); 
            OnActualCounterValueUpdateEvent.Invoke(-1);
        }
    }
    
    public IEnumerator ReloadCounterCorutine()
    {
        while (!isActive || isInBurnout)
        {
            yield return new WaitForSeconds(1); 
            OnActualCounterValueUpdateEvent.Invoke(1);
        }
    }

    public void ShieldIsReloaded()
    {
        StopAllCoroutines();
        ShieldOkIcon.SetActive(true);
        ShieldBrokenIcon.SetActive(false);
        if (BurnOutOffSound)
        {
            _audioSource.PlayOneShot(BurnOutOffSound);
        }
    }

    public void ToggleBurnoutMode()
    {
        ShieldOkIcon.SetActive(false);
        ShieldBrokenIcon.SetActive(true);
        if (BurnOutOnSound)
        {
            _audioSource.PlayOneShot(BurnOutOnSound);
        }
        OnBurnoutShieldDeactivation();
    }
    

    public void UpdateTextLabel(int activeTimeLeft)
    {
        if (isInBurnout)
        {
            counterLabel.color = Color.red;
        }

        else
        {
            counterLabel.color = Color.blue;
        }
            
        counterLabel.text = "" + activeTimeLeft;
    }

    public void ActivateGameFreezeStatus(ActivateGameFreezeCommand activateGameFreezeCommand)
    {
        _isGameFrozen = true;
        OnBurnoutShieldDeactivation();
    }
    
    public void DeactivateGameFreezeStatus(DeactivateGameFreezeCommand deactivateGameFreezeCommand)
    {
        _isGameFrozen = false;
    }

    //  Methods ---------------------------------------

    public void OnObjectHitted()
    {
        Debug.Log("Shield Hitted");
        OnShieldHittedEvent.Invoke();
    }
    

    //  Event Handlers --------------------------------
    
}
