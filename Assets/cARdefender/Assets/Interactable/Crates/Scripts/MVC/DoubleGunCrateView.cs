using System;
using System.Collections;
using System.Collections.Generic;
using cARdefender.Assets.Enemies.Generic_Enemies.Scripts;
using cARdefender.Tests.BoxPlacement;
using RMC.Core.Architectures.Mini.Context;
using RMC.Core.Architectures.Mini.View;
using UnityEngine;
using UnityEngine.Events;

public class DoubleGunCrateView : CrateView
{
    //  Fields ----------------------------------------
    [SerializeField] private int powerUpDuration;
    //  Initialization  -------------------------------

    //  Unity Methods ---------------------------------

    //  Methods ---------------------------------------

    public override void OnObjectHitted()
    {
        base.OnObjectHitted();
        OnDoubleGunCrateHittedEvent.Invoke(powerUpDuration);
    }
    


    //  Event Handlers --------------------------------
    
}
