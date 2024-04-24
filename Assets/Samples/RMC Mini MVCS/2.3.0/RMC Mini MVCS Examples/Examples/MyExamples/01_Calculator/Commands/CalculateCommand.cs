using System.Collections;
using System.Collections.Generic;
using RMC.Core.Architectures.Mini.Controller.Commands;
using UnityEngine;

public class CalculateCommand : ICommand
{
    //  Properties ------------------------------------
    public float aValue { get { return _aValue;}}
    public float bValue { get { return _bValue;}}
    public float sumValue { get { return _sumValue;}}
        
    //  Fields ----------------------------------------
    private readonly float _aValue,_bValue,_sumValue;
        
    //  Initialization  -------------------------------
    public CalculateCommand(float aValue,float bValue,float sumValue)
    {
        _aValue = aValue;
        _bValue = bValue;
        _sumValue = sumValue;
    }
}
