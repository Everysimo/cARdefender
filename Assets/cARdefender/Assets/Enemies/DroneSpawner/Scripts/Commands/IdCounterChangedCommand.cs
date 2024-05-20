using RMC.Core.Architectures.Mini.Controller.Commands;

namespace RMC.Core.Architectures.Mini.Samples.SpawnerMini.WithMini.Mini.Controller.Commands
{
    //  Namespace Properties ------------------------------

    //  Class Attributes ----------------------------------

    /// <summary>
    /// This Command is a stand-alone object containing
    /// the before and after value during a data change
    /// </summary>
    public class IdCounterChangedCommand : ValueChangedCommand<int>
    {

        //  Properties ------------------------------------
        
        //  Fields ----------------------------------------
        
        //  Initialization  -------------------------------
        public IdCounterChangedCommand(int previousValue, int currentValue) : 
            base(previousValue, currentValue)
        {
        }
    }
}