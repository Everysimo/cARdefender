using System.Collections;
using System.Collections.Generic;
using RMC.Core.Architectures.Mini.Controller.Commands;
using UnityEngine;

public class PlayerRecoverLifeCommand : ICommand
{
    //  Properties ------------------------------------

    public float LifeToRecover;
        
    //  Fields ----------------------------------------

        
    //  Initialization  -------------------------------
    public PlayerRecoverLifeCommand(float lifeToRecover)
    {
        LifeToRecover = lifeToRecover;
    }
    
    public PlayerRecoverLifeCommand()
    {
    }
}
