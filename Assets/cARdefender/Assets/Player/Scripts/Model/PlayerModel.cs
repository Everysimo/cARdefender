using RMC.Core.Architectures.Mini.Context;
using RMC.Core.Architectures.Mini.Model;

//  Namespace Properties ------------------------------

//  Class Attributes ----------------------------------

namespace cARdefender.Assets.Enemies.Drone.Scripts.Model
{
    /// <summary>
    /// The Model stores runtime data 
    /// </summary>
    public class PlayerModel : BaseModel
    {
        //  Events ----------------------------------------


        //  Properties ------------------------------------
        public Observable<float> Life { get { return _life;} }
        
    
        
        //  Fields ----------------------------------------
        private readonly Observable<float> _life = new Observable<float>();
        

        
        //  Initialization  -------------------------------
        public override void Initialize(IContext context) 
        {
            if (!IsInitialized)
            {
                base.Initialize(context);

                // Set Defaults
                _life.Value = 100f;
            }
        }
        
        //  Methods ---------------------------------------

        public void SetPlayerStats(float droneLife)
        {

            _life.Value = droneLife;
        }

        //  Event Handlers --------------------------------

    }
}
