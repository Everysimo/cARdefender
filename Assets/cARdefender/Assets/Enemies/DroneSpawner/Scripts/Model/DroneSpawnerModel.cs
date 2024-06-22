using System.Collections;
using System.Collections.Generic;
using RMC.Core.Architectures.Mini.Context;
using RMC.Core.Architectures.Mini.Model;
using UnityEngine;

public class DroneSpawnerModel : BaseModel // Extending 'base' is optional
{
    //  Events ----------------------------------------


    //  Properties ------------------------------------
    public Observable<int> AliveDronesCounter { get { return _aliveDronesCounter;} }
    
    public Observable<int> MaxAliveDrones { get { return _maxAliveDrones;} }
    
        
    //  Fields ----------------------------------------
    private readonly Observable<int> _aliveDronesCounter = new Observable<int>();
    
    private readonly Observable<int> _maxAliveDrones = new Observable<int>();
        
    //  Initialization  -------------------------------
    public override void Initialize(IContext context) 
    {
        if (!IsInitialized)
        {
            base.Initialize(context);

            // Set Defaults
            _aliveDronesCounter.Value = 0;
            _maxAliveDrones.Value = 1;
        }
    }
        
    //  Methods ---------------------------------------


    //  Event Handlers --------------------------------
    
}
