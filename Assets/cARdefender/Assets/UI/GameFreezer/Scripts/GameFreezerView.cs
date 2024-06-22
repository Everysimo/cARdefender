using System;
using System.Collections;
using System.Collections.Generic;
using RMC.Core.Architectures.Mini.Context;
using RMC.Core.Architectures.Mini.View;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameFreezeUnityEvent : UnityEvent{}
public class PasswordInsertUnityEvent : UnityEvent{}
public class GameFreezerView : MonoBehaviour,IView
{
    //Events
    [HideInInspector] public readonly GameFreezeUnityEvent GameFreezeRequest = new GameFreezeUnityEvent();
    [HideInInspector] public readonly PasswordInsertUnityEvent PasswordInsert = new PasswordInsertUnityEvent();
    
    //  Properties ------------------------------------
    
    private Coroutine _CooldownTime;
    

    
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

    public GameObject gameFreezeUI;
    
    public Sprite[] rightSigns;
    public Sprite[] leftSigns;

    public Image rightImage;
    public Image leftImage;

    public Sprite correctSprite;
    public Sprite wrongSprite;
    
    public Image leftIcon;
    public Image rightIcon;

    private int _rightPassword, _leftPassword;
    private int _rightInputValue , _leftInputValue;

    private bool _isGameFreezable;
    public int coolDownTime;
    
    [SerializeField] private AudioClip GameFreezeOnSound;
    [SerializeField] private AudioClip GameFreezeOffSound;
    
    [SerializeField] private AudioSource _audioSource;

    //  Initialization  -------------------------------
    public void Initialize(IContext context)
    {
        if (!IsInitialized)
        {
            _isInitialized = true;
            _context = context;

            _isGameFreezable = true;

            ResetInput();
            //
            
            Context.CommandManager.AddCommandListener<DeactivateGameFreezeCommand>(DeactivateFreezeGame);
            //

        }
    }

    //  Unity Methods ---------------------------------

    
    //  Methods ---------------------------------------
    public void OnGameFreezeRequest()
    {
        if (!_isGameFreezable)
        {
            return;
        }
        GameFreezeRequest.Invoke();
    }

    public void ActivateFreezeGame(int leftPassword, int rightPassword)
    {
        if (GameFreezeOnSound)
        {
            _audioSource.PlayOneShot(GameFreezeOnSound);
        }
        _isGameFreezable = false;
        
        leftImage.sprite = leftSigns[leftPassword];
        rightImage.sprite = rightSigns[rightPassword];

        _leftPassword = leftPassword;
        _rightPassword = rightPassword;
        
        gameFreezeUI.SetActive(true);
    }

    public void InsertLeftValue(int value)
    {
        _leftInputValue = value;
        leftIcon.sprite = leftSigns[_leftInputValue];
        if (_leftInputValue == _leftPassword)
        {
            leftImage.color = Color.green;
            leftIcon.color = Color.green;
            CheckInputOnPassword();
        }
        else
        {
            leftImage.color = Color.red;
            leftIcon.color = Color.red;
        }
    }
    
    public void InsertRightValue(int value)
    {
        _rightInputValue = value;
        rightIcon.sprite = rightSigns[_rightInputValue];
        
        if (_rightInputValue == _rightPassword)
        {
            rightImage.color = Color.green;
            rightIcon.color = Color.green;
            CheckInputOnPassword();
        }
        else
        {
            rightImage.color = Color.red;
            rightIcon.color = Color.red;
        }
    }

    public void CheckInputOnPassword()
    {
        if (_rightInputValue == _rightPassword && _leftInputValue == _leftPassword)
        {
            PasswordInsert.Invoke();
        }
    }

    public void DeactivateFreezeGame(DeactivateGameFreezeCommand deactivateGameFreezeCommand)
    {
        if (GameFreezeOffSound)
        {
            _audioSource.PlayOneShot(GameFreezeOffSound);
        }
        gameFreezeUI.SetActive(false);
        ResetInput();
        _CooldownTime = StartCoroutine(SetGameNotFreezable());
    }

    public IEnumerator SetGameNotFreezable()
    {
       
        yield return new WaitForSeconds(coolDownTime);
        _isGameFreezable = true;
        
    }

    private void ResetInput()
    {
        _rightInputValue = -1;
        _leftInputValue = -1;
        rightImage.color = Color.red;
        rightIcon.color = Color.red;
        
        leftImage.color = Color.red;
        leftIcon.color = Color.red;
    }
    

    //  Event Handlers --------------------------------
    
}
