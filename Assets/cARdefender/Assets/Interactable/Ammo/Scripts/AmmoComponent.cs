using System;
using cARdefender.Assets.Enemies.Generic_Enemies.Scripts;
using UnityEngine;

namespace cARdefender.Assets.Interactable.Ammo.Scripts
{
    public class AmmoComponent : MonoBehaviour
    {
        [SerializeField] public float ammoDamage;
        [SerializeField] public bool isPlayerShooting;
        
        [SerializeField] private GameObject HitPrefab;

        private bool hasHitted;
        private Transform tmp;

        private void OnTriggerEnter(Collider other)
        {
            hasHitted = false;
            
            IHittableEnemy enemyHittedView = other.transform.GetComponent<IHittableEnemy>();
            if (enemyHittedView == null)
            {
                tmp = other.transform.parent;
                while (tmp != null && enemyHittedView==null)
                {
                    enemyHittedView = tmp.GetComponentInParent<IHittableEnemy>();
                    tmp = tmp.parent;
                }
            }
            if(enemyHittedView != null)
            {
                enemyHittedView.OnTakeDamage(ammoDamage);
                hasHitted = true;
            }

            IHittableObject objectHittedView = other.transform.GetComponent<IHittableObject>();
            if (objectHittedView == null)
            {
                objectHittedView = other.transform.GetComponentInParent<IHittableObject>();
            }
            if(objectHittedView != null)
            {
                if(isPlayerShooting)
                    objectHittedView.OnObjectHitted();
                hasHitted = true;
            }
            
            ShieldView shieldView = other.transform.GetComponent<ShieldView>();
            if (shieldView == null)
            {
                shieldView = other.transform.GetComponentInParent<ShieldView>();
            }
            if(shieldView != null)
            {
                shieldView.OnObjectHitted();
                hasHitted = true;
            }

            if (hasHitted)
            {
                if (HitPrefab)
                {
                    Instantiate(HitPrefab, transform.position, Quaternion.identity);
                }
                Destroy(gameObject);
            }
        }
        
    }
}
