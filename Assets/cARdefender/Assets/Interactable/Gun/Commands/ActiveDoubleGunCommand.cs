using System.Collections;
using System.Collections.Generic;
using RMC.Core.Architectures.Mini.Controller.Commands;
using UnityEngine;

public class ActiveDoubleGunCommand : ICommand
{
    //  Properties ------------------------------------
    public int _duration;
    //  Fields ----------------------------------------

        
    //  Initialization  -------------------------------
    
    public ActiveDoubleGunCommand(int duration)
    {
        _duration = duration;
    }
    
    public ActiveDoubleGunCommand()
    {
    }
}
