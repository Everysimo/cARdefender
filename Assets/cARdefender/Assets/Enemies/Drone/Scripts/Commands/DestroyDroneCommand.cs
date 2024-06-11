using System.Collections;
using System.Collections.Generic;
using RMC.Core.Architectures.Mini.Controller.Commands;
using UnityEngine;

public class DestroyDroneCommand : ICommand
{
    //  Properties ------------------------------------
    public int dronePoints;
        
    //  Fields ----------------------------------------

        
    //  Initialization  -------------------------------
    public DestroyDroneCommand(int newDronePoints)
    {
        dronePoints = newDronePoints;
    }
    
    public DestroyDroneCommand()
    {
    }
}
