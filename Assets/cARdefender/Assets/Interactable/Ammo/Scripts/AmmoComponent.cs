using System;
using cARdefender.Assets.Enemies.Generic_Enemies.Scripts;
using UnityEngine;

namespace cARdefender.Assets.Interactable.Ammo.Scripts
{
    public class AmmoComponent : MonoBehaviour
    {
        [SerializeField] public float ammoDamage;
        [SerializeField] public bool isPlayerShooting;


        private void OnTriggerEnter(Collider other)
        {
            IHittableEnemy enemyHittedView = other.transform.GetComponent<IHittableEnemy>();
            if(enemyHittedView != null)
            {
                enemyHittedView.OnTakeDamage(ammoDamage);
                Destroy(gameObject);
            }

            IHittableObject objectHittedView = other.transform.GetComponent<IHittableObject>();
            if(objectHittedView != null)
            {
                if(isPlayerShooting)
                    objectHittedView.OnObjectHitted();
                Destroy(gameObject);
            }
        }
        
    }
}
