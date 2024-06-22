using System;
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
public class ShieldModel : IModel
{
    //  Events ----------------------------------------


    //  Properties ------------------------------------
    public bool IsInitialized { get { return _isInitialized;} }
    public IContext Context { get { return _context;} }
        
    //  Fields ----------------------------------------
    private bool _isInitialized = false;
        
    //Context
    private IContext _context;
    
    public Observable<int> MaxCounterTime { get { return _maxCounterTime;} }
    
    public Observable<int> ActualCounterTime { get { return _actualCounterTime;} }
    
    public Observable<bool> IsInBurnout { get { return _isInBurnout;} }
    
    
        
    //  Fields ----------------------------------------
    private readonly Observable<int> _maxCounterTime = new Observable<int>();
    private readonly Observable<int> _actualCounterTime = new Observable<int>();
    private readonly Observable<bool> _isInBurnout = new Observable<bool>();

        
    //  Initialization  -------------------------------
    public void Initialize(IContext context) 
    {
        if (!IsInitialized)
        {
            _isInitialized = true;
            _context = context;

            _isInBurnout.Value = false;

        }
    }

    public void RequireIsInitialized()
    {
        if (!_isInitialized)
        {
            throw new Exception("MustBeInitialized");
        }
    }
    

    //  Methods ---------------------------------------


    //  Event Handlers --------------------------------

}
