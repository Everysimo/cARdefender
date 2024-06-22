using System.Collections;
using System.Collections.Generic;
using cARdefender.Tests.BoxPlacement;
using UnityEngine;

public class OnVehicleFoundEnableShooting : MonoBehaviour
{
    // Start is called before the first frame update

    public BoxConsumer boxConsumer;
    public DroneView droneView;
    
    
    void Start()
    {
        boxConsumer.OnBoxObtainedAddListener(StartShooting);
        boxConsumer.OnBoxLost.AddListener(StopShooting);
    }

    // Update is called once per frame
    private void StartShooting(BoxInformation _)
    {
        droneView.isAttachedToVehicle = true;
    }

    private void StopShooting()
    {
        droneView.isAttachedToVehicle = false;
    }
}
