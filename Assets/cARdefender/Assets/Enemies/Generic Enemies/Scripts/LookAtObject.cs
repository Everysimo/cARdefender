using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtObject : MonoBehaviour
{
    public Transform objectThatLooks;
    public Transform objectToLook;

    [SerializeField] private float adjustingAngleX;
    [SerializeField] private float adjustingAngleY;
    [SerializeField] private float adjustingAngleZ;

    private void Update()
    {
        objectThatLooks.transform.LookAt(objectToLook);
        objectThatLooks.transform.Rotate(adjustingAngleX, adjustingAngleY, adjustingAngleZ);
    }
}
