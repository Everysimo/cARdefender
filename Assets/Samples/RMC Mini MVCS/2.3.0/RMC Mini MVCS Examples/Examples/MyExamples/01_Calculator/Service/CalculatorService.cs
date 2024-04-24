using System.Collections;
using System.Collections.Generic;
using RMC.Core.Architectures.Mini.Context;
using RMC.Core.Architectures.Mini.Service;
using UnityEngine;
using UnityEngine.Events;

public class OnSumCalculatedEvent : UnityEvent<float,float,float> {}
public class CalculatorService : BaseService
{
    //  Events ----------------------------------------
    public readonly OnSumCalculatedEvent OnSumCalculatedEvent = new OnSumCalculatedEvent();

    //  Properties ------------------------------------
        
    //  Fields ----------------------------------------

    //  Initialization  -------------------------------
    public override void Initialize(IContext context)
    {
        if (!IsInitialized)
        {
            base.Initialize(context);
        }
    }

    //  Methods ---------------------------------------

    public void external_sum(float a, float b)
    {
        float result_sum = a + b;
        
        OnSumCalculatedEvent.Invoke(a,b,result_sum);
    }
    
    //  Event Handlers --------------------------------
    
}
