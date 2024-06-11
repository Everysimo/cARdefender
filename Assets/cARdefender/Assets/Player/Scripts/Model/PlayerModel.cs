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
        public Observable<float> MaxLife { get { return _maxLife;} }
        
    
        
        //  Fields ----------------------------------------
        private readonly Observable<float> _life = new Observable<float>();
        
        private readonly Observable<float> _maxLife = new Observable<float>();
        
        //  Initialization  -------------------------------
        public override void Initialize(IContext context) 
        {
            if (!IsInitialized)
            {
                base.Initialize(context);

                // Set Defaults
                SetPlayerStats(100);
            }
        }
        
        //  Methods ---------------------------------------

        public void SetPlayerStats(float droneLife)
        {

            _life.Value = droneLife;
            _maxLife.Value = droneLife;
        }

        //  Event Handlers --------------------------------

    }
}
