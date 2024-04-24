using RMC.Core.Architectures.Mini.Context;
using RMC.Core.Architectures.Mini.Model;

//  Namespace Properties ------------------------------

//  Class Attributes ----------------------------------

namespace cARdefender.Assets.Enemies.Drone.Scripts.Model
{
    /// <summary>
    /// The Model stores runtime data 
    /// </summary>
    public class DroneModel : BaseModel
    {
        //  Events ----------------------------------------


        //  Properties ------------------------------------
        public Observable<float> Life { get { return _life;} }
    
        public Observable<float> MovementSpeed { get { return _movementSpeed;} }
    
        public Observable<float> ShootDamage { get { return _shootDamage;} }
        public Observable<float> ShootSpeed { get { return _shootSpeed;} }
    
        
        //  Fields ----------------------------------------
        private readonly Observable<float> _life = new Observable<float>();
        private readonly Observable<float> _movementSpeed = new Observable<float>();
        private readonly Observable<float> _shootDamage = new Observable<float>();
        private readonly Observable<float> _shootSpeed = new Observable<float>();
        

        
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

        public void SetDroneStats(float droneLife, float movementSpeed,float shootDamage, float shootSpeed)
        {

            _life.Value = droneLife;
            _movementSpeed.Value = movementSpeed;
            _shootDamage.Value = shootDamage;
            _shootSpeed.Value = shootSpeed;
        }

        //  Event Handlers --------------------------------

    }
}
