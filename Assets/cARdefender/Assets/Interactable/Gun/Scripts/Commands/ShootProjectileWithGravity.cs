using cARdefender.Assets.Interactable.Ammo.Scripts;
using RMC.Core.Architectures.Mini.Controller.Commands;
using UnityEngine;

namespace cARdefender.Assets.Interactable.Gun.Scripts.Commands
{
    public class ShootProjectileWithGravity : MonoBehaviour,ICommand

    {
        public float ShootSpeed { get { return _shootSpeed;}  }
        public GameObject ProjectilePrefab { get { return _projectilePrefab;} }
        
        public Transform StartPoint { get { return _startPoint;} }

        private float _shootSpeed;
        private GameObject _projectilePrefab;
        private Transform _startPoint;
        
        
        public ShootProjectileWithGravity(float shootSpeed, GameObject projectilePrefab, Transform startPoint, float gunDamage)
        {
            _shootSpeed = shootSpeed;
            _projectilePrefab = projectilePrefab;
            _startPoint = startPoint;
            
            GameObject newObject = Instantiate(ProjectilePrefab, _startPoint.position, _startPoint.rotation, null);

            if (newObject.TryGetComponent(out AmmoComponent ammoComponent))
            {
                ammoComponent.ammoDamage = gunDamage;
            }
                
            if (newObject.TryGetComponent(out Rigidbody rigidBody))
                ApplyForce(rigidBody);
        }

        void ApplyForce(Rigidbody rigidBody)
        {
            Vector3 force = _startPoint.forward * _shootSpeed;
            rigidBody.AddForce(force);
        }
    }
}