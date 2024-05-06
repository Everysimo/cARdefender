using System;
using UnityEngine;

namespace cARdefender.Assets.Interactable.Ammo.Scripts
{
    public class AmmoComponent : MonoBehaviour
    {
        [SerializeField] public float ammoDamage;


        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("Oggetto Colpito");
            IHittableEnemy objectHittedView = other.transform.GetComponent<IHittableEnemy>();
            if(objectHittedView != null)
            {
                Debug.Log("Oggetto Colpito "+objectHittedView.GetType());
                objectHittedView.OnTakeDamage(ammoDamage);
                
            }
        }
        
    }
}
