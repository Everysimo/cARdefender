using System;
using cARdefender.Tests.RemoteBoundingBoxs;
using UnityEngine;

[Serializable]
public struct ObjectRecognized
{
    public Vector3 point0;
    public Vector3 point1;
    public Vector3 point2;
    public Vector3 point3;
    public Vector3 point4;
    public Vector3 point5;
    public Vector3 point6;
    public Vector3 point7;
    public Vector3 position;
    public int id;
    public ObjectClass label;


    public Bounds GetBounds(Quaternion cameraRotation)
    {
        Quaternion pitchrot = Quaternion.Euler(cameraRotation.eulerAngles.x, 0f, cameraRotation.eulerAngles.z);

        Vector3 leftbottomback = pitchrot * point5; //Shorthand.
        Vector3 righttopfront = pitchrot * point3; //Shorthand.

        float xsize = Mathf.Abs(righttopfront.x - leftbottomback.x);
        float ysize = Mathf.Abs(righttopfront.y - leftbottomback.y);
        float zsize = Mathf.Abs(righttopfront.z - leftbottomback.z);
        return new Bounds(Vector3.zero, new Vector3(xsize, ysize, zsize));
    }

    public Vector3 Get3DWorldPosition(Vector3 camPositionAtDetection, Quaternion camRotationAtDetection)
    {
        //Get the center of the transformed bounding box. 
        float ypos =
            (localToWorld(point0, camPositionAtDetection, camRotationAtDetection).y -
             localToWorld(point4, camPositionAtDetection, camRotationAtDetection).y) / 2f +
            localToWorld(point4, camPositionAtDetection, camRotationAtDetection).y;
        Vector3 transformedroot = localToWorld(position, camPositionAtDetection, camRotationAtDetection);

        return new Vector3(transformedroot.x, ypos, transformedroot.z);
    }

    public Quaternion Get3DWorldRotation(Quaternion camRotationAtDetection)
    {
        Vector3 normal;

        normal = camRotationAtDetection * Vector3.forward; //This is to face the inverse of the camera's Z direction. 
        normal.y = 0;
        return Quaternion.LookRotation(normal, Vector3.up);
    }

    private Vector3 localToWorld(Vector3 localPos, Vector3 camPositionAtDetection, Quaternion camRotationAtDetection)
    {
        return camRotationAtDetection * localPos + camPositionAtDetection;
    }
}