using System.Collections;
using System.Collections.Generic;
using RMC.Core.Architectures.Mini.Context;
using RMC.Core.Architectures.Mini.Model;
using UnityEngine;

public class DroneSpawnerModel : BaseModel // Extending 'base' is optional
{
    //  Events ----------------------------------------


    //  Properties ------------------------------------
    public Observable<int> IdCounter { get { return _idCounter;} }
    
        
    //  Fields ----------------------------------------
    private readonly Observable<int> _idCounter = new Observable<int>();
        
    //  Initialization  -------------------------------
    public override void Initialize(IContext context) 
    {
        if (!IsInitialized)
        {
            base.Initialize(context);

            // Set Defaults
            _idCounter.Value = 0;
        }
    }
        
    //  Methods ---------------------------------------


    //  Event Handlers --------------------------------
    
}
