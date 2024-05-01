using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlaceObjectOnARandomBoundingBox : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform objectToMoveTransform;
    public float heightOffset;
    public ZED3DObjectVisualizer zed3DObjectVisualizer;

    private int currentObject = -1;
    
    // Update is called once per frame
    void Update()
    {
        MoveObject();
    }


    public void MoveObject()
    {
        Transform boxTransform = ChooseBox();
        if (boxTransform == null)
        {
            return;
        }
        
        objectToMoveTransform.position =
            boxTransform.position + Vector3.up * (boxTransform.lossyScale.y/2 + heightOffset);
    }

    private Transform ChooseBox()
    {
        if (zed3DObjectVisualizer.liveBBoxes.ContainsKey(currentObject))
        {
            return zed3DObjectVisualizer.liveBBoxes[currentObject].transform;
        }

        int count = zed3DObjectVisualizer.liveBBoxes.Count;
        if (count == 0)
        {
            return null;
        }

        int current = Random.Range(0, count);
        currentObject = zed3DObjectVisualizer.liveBBoxes.Keys.ToList()[current];
        return zed3DObjectVisualizer.liveBBoxes[currentObject].transform;


    }
}
