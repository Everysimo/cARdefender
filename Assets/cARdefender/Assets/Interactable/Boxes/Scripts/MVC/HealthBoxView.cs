using System;
using System.Collections;
using System.Collections.Generic;
using cARdefender.Assets.Enemies.Generic_Enemies.Scripts;
using cARdefender.Tests.BoxPlacement;
using RMC.Core.Architectures.Mini.Context;
using RMC.Core.Architectures.Mini.View;
using UnityEngine;
using UnityEngine.Events;

public class HealthBoxView : BoxView
{
    //  Fields ----------------------------------------

    [SerializeField]
    private float lifeToRecover;
    

    //  Initialization  -------------------------------

    //  Unity Methods ---------------------------------

    //  Methods ---------------------------------------

    public override void OnObjectHitted()
    {
        base.OnObjectHitted();
        OnHealthBoxHittedEvent.Invoke(lifeToRecover);
    }
    


    //  Event Handlers --------------------------------
    
}
