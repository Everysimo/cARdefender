using cARdefender.Assets.Interactable.Ammo.Scripts;
using RMC.Core.Architectures.Mini.Controller.Commands;
using UnityEngine;

namespace cARdefender.Assets.Interactable.Gun.Scripts.Commands
{
    public class ShootProjectileToTargetCommand : MonoBehaviour,ICommand

    {
        public float ShootSpeed { get { return _shootSpeed;}  }
        public GameObject ProjectilePrefab { get { return _projectilePrefab;} }
        
        public Transform StartPoint { get { return _startPoint;} }
        
        public Transform Target { get { return _target;} }
      
        private float _shootSpeed;
        private GameObject _projectilePrefab;
        private Transform _startPoint;
        private Transform _target;

        public ShootProjectileToTargetCommand(float shootSpeed,float shootDamage, GameObject projectilePrefab, Transform startPoint,Transform target)
        {
            _shootSpeed = shootSpeed;
            _projectilePrefab = projectilePrefab;
            _startPoint = startPoint;
            _target = target;
            
            GameObject shootProjectile = Instantiate(_projectilePrefab,_startPoint.position,Quaternion.identity);
            if (shootProjectile.TryGetComponent(out AmmoComponent ammoComponent))
            {
                ammoComponent.ammoDamage = shootSpeed;
            }
            
            shootProjectile.transform.LookAt(_target);
            shootProjectile.transform.Rotate( -90, -90, 0 );
            MoveObjectWithVelocity moveObjectWithVelocity = shootProjectile.GetComponent<MoveObjectWithVelocity>();
            Vector3 direction = (_target.position - _startPoint.position).normalized;
            moveObjectWithVelocity.velocity = direction * shootSpeed;
        }
        
        public ShootProjectileToTargetCommand(float shootSpeed, GameObject projectilePrefab, Transform startPoint,Transform target)
        {
            _shootSpeed = shootSpeed;
            _projectilePrefab = projectilePrefab;
            _startPoint = startPoint;
            _target = target;
            
            GameObject shootProjectile = Instantiate(_projectilePrefab,_startPoint.position,Quaternion.identity);
            shootProjectile.transform.LookAt(_target);
            shootProjectile.transform.Rotate( -90, -90, 0 );
            MoveObjectWithVelocity moveObjectWithVelocity = shootProjectile.GetComponent<MoveObjectWithVelocity>();
            Vector3 direction = (_target.position - _startPoint.position).normalized;
            moveObjectWithVelocity.velocity = direction * shootSpeed;
        }
    }
}