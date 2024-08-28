using System;
using System.Collections;
using System.Collections.Generic;
using cARdefender.Assets.Enemies.Generic_Enemies.Scripts;
using cARdefender.Tests.BoxPlacement;
using RMC.Core.Architectures.Mini.Context;
using RMC.Core.Architectures.Mini.View;
using UnityEngine;
using UnityEngine.Events;

public class AutoAimCrateView : CrateView
{
    //  Fields ----------------------------------------

    [SerializeField]
    private int powerUpTime;
    

    //  Initialization  -------------------------------

    //  Unity Methods ---------------------------------

    //  Methods ---------------------------------------

    public override void OnObjectHitted()
    {
        base.OnObjectHitted();
        OnAutoAimCrateHittedEvent.Invoke(powerUpTime);
    }
    


    //  Event Handlers --------------------------------
    
}
