using System;
using System.Collections;
using System.Collections.Generic;
using RMC.Core.Architectures.Mini.Context;
using RMC.Core.Architectures.Mini.Model;
using UnityEngine;

public class GameFreezerModel : IModel
{
    //  Events ----------------------------------------


    //  Properties ------------------------------------
    public bool IsInitialized { get { return _isInitialized;} }
    public IContext Context { get { return _context;} }
        
    //  Fields ----------------------------------------
    private bool _isInitialized = false;
        
    //Context
    private IContext _context;
    
    public Observable<int> LeftSignsMax { get { return _leftSignsMax;} }
    
    public Observable<int> RightSignsMax { get { return _rightSignsMax;} }
    
    //public Observable<bool> IsInBurnout { get { return _isInBurnout;} }
    
    
        
    //  Fields ----------------------------------------
    private readonly Observable<int> _leftSignsMax = new Observable<int>();
    private readonly Observable<int> _rightSignsMax = new Observable<int>();
    //private readonly Observable<bool> _isInBurnout = new Observable<bool>();

        
    //  Initialization  -------------------------------
    public void Initialize(IContext context) 
    {
        if (!IsInitialized)
        {
            _isInitialized = true;
            _context = context;

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
