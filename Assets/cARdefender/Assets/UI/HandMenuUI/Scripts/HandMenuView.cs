using System;
using System.Collections;
using System.Collections.Generic;
using RMC.Core.Architectures.Mini.Context;
using RMC.Core.Architectures.Mini.Samples.RollABall.WithMini.Components;
using RMC.Core.Architectures.Mini.Samples.RollABall.WithMini.Mini.Controller.Commands;
using RMC.Core.Architectures.Mini.View;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

//  Namespace Properties ------------------------------

//  Class Attributes ----------------------------------

/// <summary>
/// The View handles user interface and user input
/// </summary>
public class HandMenuView : MonoBehaviour, IView
{
    //  Events ----------------------------------------
    

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
    private TMP_Text playerLifeText;
    
    [SerializeField]
    [Tooltip("Health Sprite")]
    private Image healthBarSprite = null;

    private float _target = 1;

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

    //  Unity Methods ---------------------------------
    
    

    public void UpdatePlayerLifeUI(float life,float maxLife)
    {
        playerLifeText.text = life+"/"+maxLife;
    }
    
    private void Update()
    {
        
        healthBarSprite.fillAmount = Mathf.MoveTowards( healthBarSprite.fillAmount,_target,2*Time.deltaTime);
    }


    //  Methods ---------------------------------------
    
    public void UpdateHealthBar(float currentLife,float maxLife)
    {
        _target = currentLife/maxLife;
    }

    //  Event Handlers --------------------------------
    
}