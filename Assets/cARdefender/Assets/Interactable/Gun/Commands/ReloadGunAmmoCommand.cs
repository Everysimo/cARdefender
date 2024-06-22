using System.Collections;
using System.Collections.Generic;
using RMC.Core.Architectures.Mini.Controller.Commands;
using UnityEngine;

public class ReloadGunAmmoCommand : ICommand
{
    //  Properties ------------------------------------
    public int amount;
    //  Fields ----------------------------------------

        
    //  Initialization  -------------------------------
    
    public ReloadGunAmmoCommand(int newAmount)
    {
        amount = newAmount;
    }
    
    public ReloadGunAmmoCommand()
    {
    }
}
