using System;
using UnityEngine;

namespace cARdefender.Assets.Interactable.Ammo.Scripts
{
    public class AmmoComponent : MonoBehaviour
    {
        [SerializeField] public float ammoDamage;


        private void OnTriggerEnter(Collider other)
        {
            IHittableEnemy objectHittedView = other.transform.GetComponent<IHittableEnemy>();
            if(objectHittedView != null)
            {
                objectHittedView.OnTakeDamage(ammoDamage);
                Destroy(gameObject);
            }
        }
        
    }
}
