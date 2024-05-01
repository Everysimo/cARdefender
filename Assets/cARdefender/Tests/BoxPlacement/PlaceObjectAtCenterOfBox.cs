using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceObjectAtCenterOfBox : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform boxTransform;
    public Transform objectToMoveTransform;
    public float heightOffset;
    
    // Update is called once per frame
    void Update()
    {
        MoveObject();
    }


    public void MoveObject()
    {
        objectToMoveTransform.position =
            boxTransform.position + Vector3.up * (boxTransform.lossyScale.y/2 + heightOffset);
    }
}
