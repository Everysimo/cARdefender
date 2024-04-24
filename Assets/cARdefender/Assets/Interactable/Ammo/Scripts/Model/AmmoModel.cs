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
public class AmmoModel : BaseModel
{
    //  Events ----------------------------------------


    //  Properties ------------------------------------
    public Observable<float> ShootDamage { get { return _shootDamage;} }
    
        
    //  Fields ----------------------------------------
    private readonly Observable<float> _shootDamage,_shootSpeed = new Observable<float>();
    

        
    //  Initialization  -------------------------------
    public override void Initialize(IContext context) 
    {
        if (!IsInitialized)
        {
            base.Initialize(context);

            // Set Defaults
            
            _shootDamage.Value = 1;
            _shootSpeed.Value = 1;
        }
    }
        
    //  Methods ---------------------------------------

    public void SetDroneStats(float shootDamage, float shootSpeed)
    {
        
        ShootDamage.Value = shootDamage;
        
    }

    //  Event Handlers --------------------------------

}
