using cARdefender.Tests.BoxPlacement;
using UnityEngine;

public class FixObjectOnBoundingBox : MonoBehaviour
{
    public Transform objectToMoveTransform;
    public float heightOffset;

    public BoxConsumer boxConsumer;

    // Update is called once per frame
    void Update()
    {
        MoveObject();
    }


    public void MoveObject()
    {
        BoxInformation? boxInformation = boxConsumer.GetBoxInformation();
        if (boxInformation == null) return;
        Transform boxTransform = boxInformation.Value.boxObject.transform;
        Vector3 TargetPosition = boxTransform.position + Vector3.up * (boxTransform.lossyScale.y / 2 + heightOffset);

        objectToMoveTransform.position = TargetPosition;
    }
}