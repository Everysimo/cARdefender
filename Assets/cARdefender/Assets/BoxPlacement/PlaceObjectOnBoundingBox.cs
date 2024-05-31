using cARdefender.Tests.BoxPlacement;
using UnityEngine;

public class PlaceObjectOnBoundingBox : MonoBehaviour
{
    public Transform objectToMoveTransform;
    public float heightOffset;
    public float speed;

    public BoxConsumer boxConsumer;
    
    // Update is called once per frame
    void Update()
    {
        MoveObject();
    }

    

    public void MoveObject()
    {
        if(boxConsumer.boxInformation == null) return;
        Transform boxTransform = boxConsumer.boxInformation.Value.boxObject.transform;
        Vector3 TargetPosition = boxTransform.position + Vector3.up * (boxTransform.lossyScale.y/2 + heightOffset);
        Vector3 CurrentPosition = objectToMoveTransform.position;
        Vector3 Distance = TargetPosition - CurrentPosition;
        float distanceInMeters = Distance.magnitude;
        float distanceToMove = speed * Time.deltaTime;
        if (distanceInMeters <= distanceToMove)
        {
            objectToMoveTransform.position = TargetPosition;
        }
        else
        {
            objectToMoveTransform.position += Distance.normalized * distanceToMove;
        }
    }
    
    
}
