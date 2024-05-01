using System;
using System.Collections;
using System.Collections.Generic;
using cARdefender.Assets.Enemies.Drone.Scripts.Model;
using RMC.Core.Experimental;
using UnityEngine;

public class AmmoInteractable : MonoBehaviour
{
    [SerializeField] private int ammoDamage;

    public void OnCollisionEnter(Collision other)
    {
        Debug.Log("Drone Colpito");
        DroneView droneView = other.transform.GetComponent<DroneView>();
        if(droneView != null)
        {
            Debug.Log("Drone Colpito");
            droneView.OnTakeDamage(ammoDamage);
        }
    }
}
