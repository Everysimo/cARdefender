using System;
using System.Collections;
using System.Collections.Generic;
using cARdefender.Tests.BoxPlacement;
using UnityEngine;

public class ObtainPowerupOnVeichleDetected : MonoBehaviour
{
    // Start is called before the first frame update

    public DroneView droneView;
    public BoxConsumer boxConsumer;

    private void Awake()
    {
        boxConsumer.OnBoxObtainedAddListener(CallPowerupObtained);
        boxConsumer.OnBoxLost.AddListener(droneView.LosePowerUpOnVehicleDetach);
    }

    public void CallPowerupObtained(BoxInformation boxInformation)
    {
        droneView.GainPowerUpDroneOnVehicleAttach(boxInformation.label);
    }
    
    
}
