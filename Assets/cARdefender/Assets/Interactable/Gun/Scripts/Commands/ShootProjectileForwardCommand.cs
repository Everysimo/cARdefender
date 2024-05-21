using cARdefender.Assets.Interactable.Ammo.Scripts;
using RMC.Core.Architectures.Mini.Controller.Commands;
using UnityEngine;

namespace cARdefender.Assets.Interactable.Gun.Scripts.Commands
{
    public class ShootProjectileForwardCommand : MonoBehaviour,ICommand

    {
        public float ShootSpeed { get { return _shootSpeed;}  }
        public GameObject ProjectilePrefab { get { return _projectilePrefab;} }
        
        public Transform StartPoint { get { return _startPoint;} }

        private float _shootSpeed;
        private GameObject _projectilePrefab;
        private Transform _startPoint;
        
        
        public ShootProjectileForwardCommand(float shootSpeed, GameObject projectilePrefab, Transform startPoint, float gunDamage)
        {
            _shootSpeed = shootSpeed;
            _projectilePrefab = projectilePrefab;
            _startPoint = startPoint;
            
            
            GameObject shootProjectile = Instantiate(ProjectilePrefab, _startPoint.position, Quaternion.identity);
            shootProjectile.transform.LookAt(startPoint.up.normalized);
            
            if (shootProjectile.TryGetComponent(out AmmoComponent ammoComponent))
            {
                ammoComponent.ammoDamage = gunDamage;
            }

            MoveObjectWithVelocity moveObjectWithVelocity = shootProjectile.GetComponent<MoveObjectWithVelocity>();
            Vector3 direction = startPoint.forward.normalized;
            moveObjectWithVelocity.velocity = direction * shootSpeed;
        }

        
    }
}