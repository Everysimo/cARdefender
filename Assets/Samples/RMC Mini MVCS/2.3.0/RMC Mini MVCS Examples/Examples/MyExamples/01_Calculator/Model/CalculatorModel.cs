using System.Collections;
using System.Collections.Generic;
using RMC.Core.Architectures.Mini.Context;
using RMC.Core.Architectures.Mini.Model;
using UnityEngine;

//  Namespace Properties ------------------------------

//  Class Attributes ----------------------------------

/// <summary>
/// The Model stores runtime data 
/// </summary>
public class CalculatorModel : BaseModel
{
    //  Events ----------------------------------------


    //  Properties ------------------------------------
    public Observable<float> aValue { get { return _aValue;} }
    public Observable<float> bValue { get { return _bValue;} }
    public Observable<float> sumValue { get { return _sumValue;} }
    public bool IsCalculated { get { return _sumValue.Value != 0;} }
    public Observable<bool> CanCalculate { get { return _canCalculate;} }
        
    //  Fields ----------------------------------------
    private readonly Observable<bool> _canCalculate = new Observable<bool>();
    private readonly Observable<float> _aValue,_bValue,_sumValue = new Observable<float>();

        
    //  Initialization  -------------------------------
    public override void Initialize(IContext context) 
    {
        if (!IsInitialized)
        {
            base.Initialize(context);

            // Set Defaults
            _canCalculate.Value = false;
            _sumValue.Value = 0;
        }
    }
        
    //  Methods ---------------------------------------


    //  Event Handlers --------------------------------

}
