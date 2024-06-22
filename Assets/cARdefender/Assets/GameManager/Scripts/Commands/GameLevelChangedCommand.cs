using System.Collections;
using System.Collections.Generic;
using RMC.Core.Architectures.Mini.Controller.Commands;
using UnityEngine;

public class GameLevelChangedCommand : ICommand
{
    //  Properties ------------------------------------
    public int Level;
        
    //  Fields ----------------------------------------

        
    //  Initialization  -------------------------------
    public GameLevelChangedCommand(int newLevel)
    {
        Level = newLevel;
    }
    
    public GameLevelChangedCommand()
    {
    }
}
