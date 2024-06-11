using System;
using System.Collections;
using System.Collections.Generic;
using cARdefender.Tests.BoxPlacement;
using UnityEngine;

public class RemoveBoxOnDestroy : MonoBehaviour
{
    // Start is called before the first frame update
    public BoxManager boxManager;
    public BoxInformationContainer boxInformationContainer;


    private void OnDisable()
    {
        boxManager.Boxes.Remove(boxInformationContainer.boxInformation.Id);
        boxManager.UpdateBoxes();
    }

    private void OnEnable()
    {
        boxManager.Boxes.Add(boxInformationContainer.boxInformation.Id,boxInformationContainer.boxInformation.boxObject);
        boxManager.UpdateBoxes();
    }
}
