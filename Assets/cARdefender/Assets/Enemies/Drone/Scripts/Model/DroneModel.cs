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
        
        public Observable<float> MaxLife { get { return _maxlife;} }
    
        public Observable<float> MovementSpeed { get { return _movementSpeed;} }
    
        public Observable<float> ShootDamage { get { return _shootDamage;} }
        
        public Observable<float> DefaultShootDamage { get { return _DefaultShootDamage;} }
        public Observable<float> ShootSpeed { get { return _shootSpeed;} }
        
        public Observable<int> points { get { return _points;} }
        
        public Observable<int> ammoReward { get { return _ammoReward;} }
        
        public Observable<int> id { get { return _id;} }
    
        
        //  Fields ----------------------------------------
        private readonly Observable<float> _life = new Observable<float>();
        private readonly Observable<float> _maxlife = new Observable<float>();
        private readonly Observable<float> _movementSpeed = new Observable<float>();
        
        private readonly Observable<float> _shootDamage = new Observable<float>();
        
        private readonly Observable<float> _DefaultShootDamage = new Observable<float>();
        private readonly Observable<float> _shootSpeed = new Observable<float>();
        
        private readonly Observable<int> _id = new Observable<int>();
        private readonly Observable<int> _ammoReward = new Observable<int>();
        
        private readonly Observable<int> _points = new Observable<int>();

        
        //  Initialization  -------------------------------
        public override void Initialize(IContext context) 
        {
            if (!IsInitialized)
            {
                base.Initialize(context);

                // Set Defaults
                _life.Value = 50f;
                _maxlife.Value = 50f;
                _points.Value = 50;
            }
        }
        
        //  Methods ---------------------------------------

        public void SetDroneStats(float droneLife, float movementSpeed,float shootDamage, float shootSpeed,int points)
        {

            _life.Value = droneLife;
            _maxlife.Value = droneLife;
            _movementSpeed.Value = movementSpeed;
            _shootDamage.Value = shootDamage;
            _DefaultShootDamage.Value = shootDamage;
            _shootSpeed.Value = shootSpeed;
            _points.Value = points;
        }

        //  Event Handlers --------------------------------

    }
}
