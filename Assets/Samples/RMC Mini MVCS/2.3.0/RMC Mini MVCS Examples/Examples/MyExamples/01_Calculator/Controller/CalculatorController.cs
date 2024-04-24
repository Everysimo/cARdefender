using System.Collections;
using System.Collections.Generic;
using RMC.Core.Architectures.Mini.Context;
using RMC.Core.Architectures.Mini.Controller;
using RMC.Core.Architectures.Mini.Samples.Login.WithMini.Mini.Controller.Commands;
using RMC.Core.Architectures.Mini.Samples.Login.WithMini.Mini.Model;
using UnityEngine;

//  Namespace Properties ------------------------------

//  Class Attributes ----------------------------------

/// <summary>
/// The Controller coordinates everything between
/// the <see cref="IConcern"/>s and contains the core app logic 
/// </summary>
public class CalculatorController : BaseController<CalculatorModel, CalculatorView, CalculatorService>
{
    //  Events ----------------------------------------


    //  Properties ------------------------------------


    //  Fields ----------------------------------------

    public CalculatorController(CalculatorModel model, CalculatorView view, CalculatorService service) : base(model,
        view, service)
    {
    }

    //  Initialization  -------------------------------
    public override void Initialize(IContext context)
    {
        if (!IsInitialized)
        {
            base.Initialize(context);

            //
            //_model.aValue.OnValueChanged.AddListener(Model_OnAValueChanged);
            //_model.bValue.OnValueChanged.AddListener(Model_OnBValueChanged);
            //_model.sumValue.OnValueChanged.AddListener(Model_OnSumValueChanged);
            _view.OnCalculate.AddListener(View_OnCalculate);
            _view.OnClear.AddListener(View_OnClear);
            _view.OnCanCalculateChanged.AddListener(View_OnCanCalculateChanged);
            _service.OnSumCalculatedEvent.AddListener(Service_OnSumCalculated);

            // Demo - Controller may update model DIRECTLY...
            _model.sumValue.Value = 0;

            // Clear
        }
    }

    //  Methods ---------------------------------------


    //  Event Handlers --------------------------------
    private void View_OnCanCalculateChanged(bool canCalculate)
    {
        _model.CanCalculate.Value = canCalculate;
    }


    private void View_OnCalculate(float aValue, float bValue)
    {
        RequireIsInitialized();
        
        _service.external_sum(aValue,bValue);
    }
    

    private void View_OnClear()
    {
        RequireIsInitialized();

        Context.CommandManager.InvokeCommand(new ClearCommand());
    }

    private void Service_OnSumCalculated(float a, float b, float resultSum)
    {
        RequireIsInitialized();
        
        Context.CommandManager.InvokeCommand(
            new CalculateCommand(a,b,resultSum));
    }

    
}