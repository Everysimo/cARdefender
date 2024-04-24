using System;
using System.Collections;
using System.Collections.Generic;
using RMC.Core.Architectures.Mini.Context;
using RMC.Core.Architectures.Mini.Samples.RollABall.WithMini.Components;
using RMC.Core.Architectures.Mini.Samples.RollABall.WithMini.Mini.Controller.Commands;
using RMC.Core.Architectures.Mini.View;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

//  Namespace Properties ------------------------------

//  Class Attributes ----------------------------------
public class CalculateUnityEvent : UnityEvent<float,float>{}
public class ClearUnityEvent : UnityEvent{}

public class CanCalculateChangedUnityEvent : UnityEvent<bool>{}

/// <summary>
/// The View handles user interface and user input
/// </summary>
public class CalculatorView : MonoBehaviour, IView
{
    //  Events ----------------------------------------
    [HideInInspector] public readonly CalculateUnityEvent OnCalculate = new CalculateUnityEvent();
    [HideInInspector] public readonly ClearUnityEvent OnClear = new ClearUnityEvent();
    [HideInInspector] public readonly CanCalculateChangedUnityEvent OnCanCalculateChanged = new CanCalculateChangedUnityEvent();


    //  Properties ------------------------------------
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

    [SerializeField] private TMP_InputField _inputFieldA,_inputFieldB,_inputFieldResult;

    [SerializeField] private Button _buttonClear, _buttonCalculate;

    //  Initialization  -------------------------------
    public void Initialize(IContext context)
    {
        if (!IsInitialized)
        {
            _isInitialized = true;
            _context = context;
            
            //
            _inputFieldA?.onValueChanged.AddListener(AnyInputField_OnValueChanged);
            _inputFieldB?.onValueChanged.AddListener(AnyInputField_OnValueChanged);
            _buttonClear?.onClick.AddListener(ClearButton_OnClicked);
            _buttonCalculate?.onClick.AddListener(CalculateButton_OnClicked);
            
            //
            Context.CommandManager.AddCommandListener<CalculateCommand>(
                OnCalculateCommand);
            Context.CommandManager.AddCommandListener<ClearCommand>(
                OnClearCommand);
            
            //
            _buttonCalculate.interactable = false;
        }
    }


    public void RequireIsInitialized()
    {
        if (!IsInitialized)
        {
            throw new Exception("MustBeInitialized");
        }
    }


    //  Unity Methods ---------------------------------

    private void AnyInputField_OnValueChanged(string _)
    {
        float a, b;

        bool isValidInput = _inputFieldA.text.Length > 0 && 
                            _inputFieldB.text.Length > 0 &&
                            float.TryParse(_inputFieldA.text, out a) &&
                            float.TryParse(_inputFieldB.text, out b);
            
        bool hasValidInput = _inputFieldA.text.Length > 0 || 
                             _inputFieldB.text.Length > 0;

        _buttonCalculate.interactable = isValidInput;
        _buttonClear.interactable = hasValidInput;
        //OnCanCalculateChanged.Invoke(_buttonCalculate.interactable);
    }
    
        
        
    private void CalculateButton_OnClicked()
    {
        //OnCalculate.Invoke(float.Parse(_inputFieldA.text),float.Parse(_inputFieldB.text));
    }
        
    private void ClearButton_OnClicked()
    {
        
        //OnClear.Invoke(); ;
    }
        
        
    protected void OnDestroy()
    {
        Context?.CommandManager?.RemoveCommandListener<CalculateCommand>(
            OnCalculateCommand);
        Context?.CommandManager?.RemoveCommandListener<ClearCommand>(
            OnClearCommand);
    }


    //  Methods ---------------------------------------


    //  Event Handlers --------------------------------
    private void OnCalculateCommand(CalculateCommand calculateCommand)
    {
        RequireIsInitialized();

        _inputFieldResult.text = calculateCommand.sumValue.ToString();
    }
        
    private void OnClearCommand(ClearCommand clearCommand)
    {
        RequireIsInitialized();
            
        if (_buttonCalculate != null) _buttonCalculate.interactable = false;
        if (_buttonClear != null) _buttonClear.interactable = false;
        if (_inputFieldA != null) _inputFieldA.interactable = true;
        if (_inputFieldB != null) _inputFieldB.interactable = true;
        if (_inputFieldA != null) _inputFieldA.text = "";
        if (_inputFieldB != null) _inputFieldB.text = "";
    }
}