using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalculatorMVC : MonoBehaviour
{
    //  Events ----------------------------------------


    //  Properties ------------------------------------
        
        
    //  Fields ----------------------------------------
        
    [SerializeField] 
    private CalculatorView _calculatorView;


        
    //  Unity Methods  --------------------------------
    protected void Start()
    {
        CalculatorMVCSManager calculatorMvcs = 
            new CalculatorMVCSManager(_calculatorView);
            
        calculatorMvcs.Initialize();
    }


    //  Methods ---------------------------------------


    //  Event Handlers --------------------------------
}
